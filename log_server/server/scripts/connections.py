import asyncio
import base64
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

# Folder with public keys (PEM files)
PUBLIC_KEYS_FOLDER: str = "public_keys"

MESSAGE_DELIMITER: bytes = b"\x1F"
HANDSHAKE_RESPONSE_TIMEOUT: float = 3.0

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

    try:
        # Step 1: generate random challenge
        challenge: bytes = generate_challenge()

        # Step 2: send challenge to client (challenge + delimiter)
        writer.write(challenge + MESSAGE_DELIMITER)
        await writer.drain()

        # Step 3: receive Base64-encoded signature from client
        try:
            signature_line: bytes = await asyncio.wait_for(
                reader.readuntil(MESSAGE_DELIMITER),
                timeout=HANDSHAKE_RESPONSE_TIMEOUT)
        except asyncio.TimeoutError:
            logger.warning("Handshake timeout reached for: %s",
                            client_address)
            return

        # Remove delimiter from the end, if present
        if signature_line.endswith(MESSAGE_DELIMITER):
            signature_line = signature_line[:-len(MESSAGE_DELIMITER)]

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
                data: bytes = await reader.readuntil(MESSAGE_DELIMITER)

                # Strip delimiter from the end
                if data.endswith(MESSAGE_DELIMITER):
                    data = data[:-len(MESSAGE_DELIMITER)]
                try:
                    apps_logger.write(data.decode("utf-8").strip().split('\x1E'))
                except:
                    pass
            except asyncio.IncompleteReadError:
                logger.info("Client disconnected: %s",
                            client_address)
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
        
async def start_log_server(host: str, port: int) -> None:
    """
    Start asynchronous TCP log server and run it until the application stops.

    :param host: Host/IP address on which the server will listen.
    :type host: str
    :param port: TCP port number on which the server will listen.
    :type port: int
    :returns: None. This coroutine runs until cancelled or the process exits.
    :rtype: None
    """
    server = await asyncio.start_server(handle_client, host, port)

    # Build a human-readable list of all bound socket addresses for logging
    addresses = ", ".join(str(sock.getsockname()) for sock in server.sockets)
    logger.info("Log server started. Listening on %s", addresses)

    async with server:
        await server.serve_forever()
