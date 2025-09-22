namespace LogClient
{
    public class LoggerException : Exception
    {
        public LoggerException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public LoggerException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerException"/> class with a specified 
        /// error message and a reference to the inner exception that caused this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The inner exception that is the cause of this exception.</param>
        public LoggerException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}