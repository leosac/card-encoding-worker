namespace Leosac.CredentialProvisioning.Encoding
{
    /// <summary>
    /// An encoding exception.
    /// </summary>
    public class EncodingException : Exception
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public EncodingException() : base() { }

        /// <summary>
        /// Default constructor with a custom message.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public EncodingException(string message) : base(message) { }

        /// <summary>
        /// Default constructor with a custom message and inner exception.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public EncodingException(string message, Exception? innerException) : base(message, innerException) { }
    }
}
