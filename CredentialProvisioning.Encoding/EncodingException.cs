namespace Leosac.CredentialProvisioning.Encoding
{
    public class EncodingException : Exception
    {
        public EncodingException() : base() { }

        public EncodingException(string message) : base(message) { }

        public EncodingException(string message, Exception? innerException) : base(message, innerException) { }
    }
}
