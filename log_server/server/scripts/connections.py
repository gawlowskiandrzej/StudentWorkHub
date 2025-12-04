import asyncio
import base64
import ssl
from cryptography.hazmat.primitives import hashes
from cryptography.hazmat.primitives.asymmetric import padding
from app_core import (
    logger,
    apps_logger
)
from helpers import (
    get_public_keys,
    generate_challenge
)
from pathlib import Path

# Folder with public keys (PEM files)
PUBLIC_KEYS_FOLDER: str = "public_keys"

MESSAGE_DELIMITER: bytes = b"\x1F"
HANDSHAKE_RESPONSE_TIMEOUT: float = 3.0
MAX_MESSAGE_SIZE:int = 100 * 1024

def create_tls_context(certfile: str | Path, keyfile: str | Path) -> ssl.SSLContext:
    """
    Create SSL/TLS context for server-side TLS connections.

    :param certfile: Path to the TLS certificate in PEM format.
    :type certfile: str
    :param keyfile: Path to the TLS private key in PEM format.
    :type keyfile: str
    :returns: Configured SSLContext instance.
    :rtype: ssl.SSLContext
    """
    certfile = Path(certfile).expanduser().resolve()
    if not certfile.is_file():
        logger.error("Certificate file %s doesn't exist",
                    certfile)
        raise ValueError(f"Certificate file {certfile} doesn't exist")
    
    keyfile = Path(keyfile).expanduser().resolve()
    if not keyfile.is_file():
        logger.error("Server private key file %s doesn't exist",
                    keyfile)
        raise ValueError(f"Server private key file {keyfile} doesn't exist")
    
    context: ssl.SSLContext = ssl.SSLContext(ssl.PROTOCOL_TLS_SERVER)
    context.minimum_version = ssl.TLSVersion.TLSv1_3

    # Load server certificate and private key
    try:
        context.load_cert_chain(certfile=certfile,
                                keyfile=keyfile)
    except Exception as error:
        logger.error("Failed to load TLS certificate/key: %s",
                    error)
        raise RuntimeError(f"Failed to load TLS certificate/key: {error}")
    
    return context


class MessageTooLargeError(Exception):
    """Raised when a received message exceeds the maximum allowed size."""
    pass

async def read_message_with_limit(
    reader: asyncio.StreamReader,
    delimiter: bytes,
    max_message_size: int,
    buffer: bytearray,
    chunk_size: int = 1024
) -> bytes:
    """
    Read a single message from the stream until the delimiter is found or
    the maximum allowed size is exceeded.

    The returned payload does not include the delimiter.

    :param reader: Stream reader associated with the client TCP connection.
    :type reader: asyncio.StreamReader
    :param delimiter: Message delimiter marking the end of a single message.
    :type delimiter: bytes
    :param max_message_size: Maximum allowed size of a single message payload in bytes.
    :type max_message_size: int
    :param buffer: Connection-level buffer used to store unread bytes between calls.
    :type buffer: bytearray
    :param chunk_size: Number of bytes to read from the stream per iteration.
    :type chunk_size: int
    :returns: Message payload without the delimiter.
    :rtype: bytes
    :raises asyncio.IncompleteReadError: If the connection closes before a full message is read.
    :raises MessageTooLargeError: If the message exceeds the maximum allowed size.
    """
    while True:
        delimiter_index: int = buffer.find(delimiter)
        if delimiter_index != -1:
            message: bytes = bytes(buffer[:delimiter_index])
            del buffer[:delimiter_index + len(delimiter)]
            return message

        if len(buffer) >= max_message_size:
            raise MessageTooLargeError(f"Message exceeds maximum allowed size of {max_message_size} bytes")

        chunk: bytes = await reader.read(chunk_size)
        if not chunk:
            raise asyncio.IncompleteReadError(partial=bytes(buffer), expected=len(buffer) + 1)

        buffer.extend(chunk)
        if len(buffer) > max_message_size:
            raise MessageTooLargeError(f"Message exceeds maximum allowed size of {max_message_size} bytes")
            
async def handle_client(reader: asyncio.StreamReader, writer: asyncio.StreamWriter) -> None:
    """
    Handle a single TCP client connection using challenge-response authentication.

    Authentication flow:
    1. Generate a random challenge (bytes) using generate_challenge().
    2. Send the challenge to the client, terminated by MESSAGE_DELIMITER.
    3. Wait up to HANDSHAKE_RESPONSE_TIMEOUT seconds for a Base64-encoded
       RSA signature of the challenge, also terminated by MESSAGE_DELIMITER.
    4. Try to verify the signature using all RSA public keys loaded from
       PUBLIC_KEYS_FOLDER. If none of the keys validates the signature,
       the connection is rejected.
    5. If authentication succeeds, read subsequent messages delimited by
       MESSAGE_DELIMITER in a loop. Message payloads are not logged, only
       connection and error metadata are recorded.

    :param reader: Stream reader associated with the client TCP connection.
    :type reader: asyncio.StreamReader
    :param writer: Stream writer associated with the client TCP connection.
    :type writer: asyncio.StreamWriter
    :returns: None. The connection is closed when the function finishes.
    :rtype: None
    """
    client_address = writer.get_extra_info("peername")
    logger.info("New connection from %s",
                client_address)
    
    read_buffer: bytearray = bytearray()
    try:
        # Step 1: generate random challenge
        challenge: bytes = generate_challenge()

        # Step 2: send challenge to client (challenge + delimiter)
        writer.write(challenge + MESSAGE_DELIMITER)
        await writer.drain()

        # Step 3: receive Base64-encoded signature from client
        try:
            signature_line: bytes = await asyncio.wait_for(
                read_message_with_limit(
                    reader,
                    MESSAGE_DELIMITER,
                    MAX_MESSAGE_SIZE,
                    read_buffer
                ),
                timeout=HANDSHAKE_RESPONSE_TIMEOUT)
        except asyncio.TimeoutError:
            logger.warning("Handshake timeout reached for: %s",
                            client_address)
            return
        except MessageTooLargeError:
            logger.warning("Handshake response from %s exceeds maximum allowed size (%d bytes)",
                            client_address,
                            MAX_MESSAGE_SIZE)
            return 

        if not signature_line:
            logger.warning("Client: %s returned empty handshake response",
                            client_address)
            return

        try:
            signature_b64: str = signature_line.decode("ascii")
            signature_bytes: bytes = base64.b64decode(signature_b64, validate=True)
        except Exception as error:
            logger.warning("Invalid signature format from %s: %s",
                            client_address,
                            error)
            return

        # Step 4: load public keys and try verification
        public_keys = get_public_keys(PUBLIC_KEYS_FOLDER)
        if len(public_keys) < 1:
            logger.warning("No public keys loaded, rejecting connection from %s",
                            client_address)
            return

        authenticated: bool = False
        for public_key in public_keys:
            try:
                # Verify signature of the challenge
                public_key.verify(
                    signature_bytes,
                    challenge,
                    padding.PKCS1v15(),
                    hashes.SHA256())
                authenticated = True
                break
            except Exception:
                # Try next key
                continue

        if not authenticated:
            logger.info("Authentication failed for client %s. Closing connection",
                        client_address)
            return

        logger.info("Client %s authenticated successfully",
                    client_address)

        # Step 5: after successful authentication, receive message content
        # Messages are delimited by MESSAGE_DELIMITER and their payload is not logged.
        while True:
            try:
                data: bytes = await read_message_with_limit(
                    reader,
                    MESSAGE_DELIMITER,
                    MAX_MESSAGE_SIZE,
                    read_buffer)

                try:
                    apps_logger.write(data.decode("utf-8").strip().split('\x1E'))
                except:
                    pass
            except asyncio.IncompleteReadError:
                logger.info("Client disconnected: %s",
                            client_address)
                break
            
            except MessageTooLargeError:
                logger.warning("Message from %s exceeds maximum allowed size (%d bytes). Closing connection",
                                client_address,
                                MAX_MESSAGE_SIZE)
                break
            except Exception as error:
                logger.error("Error while reading data from client %s: %s",
                            client_address,
                            error)
                break

    except Exception as error:
        logger.error("Error while handling client %s: %s",
                    client_address,
                    error)

    finally:
        writer.close()
        try:
            await writer.wait_closed()
        except Exception:
            pass
        logger.info("Connection closed: %s",
                    client_address)
        
async def start_log_server(host: str, port: int, certfile: str, keyfile: str) -> None:
    """
    Start asynchronous TCP log server (optionally over TLS) and run it
    until the application stops.

    :param host: Host/IP address on which the server will listen.
    :type host: str
    :param port: TCP port number on which the server will listen.
    :type port: int
    :param certfile: Path to the TLS certificate in PEM format.
    :type certfile: str
    :param keyfile: Path to the TLS private key in PEM format.
    :type keyfile: str
    :returns: None. This coroutine runs until cancelled or the process exits.
    :rtype: None
    """
    tls_context: ssl.SSLContext = create_tls_context(certfile, keyfile)

    server = await asyncio.start_server(
        handle_client,
        host,
        port,
        ssl=tls_context)

    # Build a human-readable list of all bound socket addresses for logging
    addresses = ", ".join(str(sock.getsockname()) for sock in server.sockets)
    logger.info("Log server started (TLS enabled). Listening on %s",
                addresses)

    async with server:
        await server.serve_forever()

