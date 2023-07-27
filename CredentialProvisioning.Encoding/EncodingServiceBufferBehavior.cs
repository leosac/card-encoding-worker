namespace Leosac.CredentialProvisioning.Encoding
{
    /// <summary>
    /// Buffer behavior on the shared temporary buffer from encoding service.
    /// </summary>
    public enum EncodingServiceBufferBehavior
    {
        /// <summary>
        /// Do nothing on the buffer data.
        /// </summary>
        DoNothing,
        /// <summary>
        /// Overwrite the current buffer value.
        /// </summary>
        Overwrite,
        /// <summary>
        /// Append additional data to the buffer.
        /// </summary>
        Append,
        /// <summary>
        /// Preprend additional data to the buffer.
        /// </summary>
        Prepend
    }
}
