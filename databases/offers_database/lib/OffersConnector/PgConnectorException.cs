namespace OffersConnector
{
    /// <summary>
    /// Custom exception used in the PgConnector class.
    /// It acts as a wrapper for other exceptions to make error handling simpler and more consistent.
    /// </summary>
    public class PgConnectorException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PgConnectorException"/> class with no message.
        /// </summary>
        public PgConnectorException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PgConnectorException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public PgConnectorException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PgConnectorException"/> class with a specified 
        /// error message and a reference to the inner exception that caused this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The inner exception that is the cause of this exception.</param>
        public PgConnectorException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
