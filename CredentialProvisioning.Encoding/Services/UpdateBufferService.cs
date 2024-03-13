namespace Leosac.CredentialProvisioning.Encoding.Services
{
    /// <summary>
    /// Update buffer encoding service properties.
    /// </summary>
    public class UpdateBufferService : EncodingServiceProperties
    {
        /// <summary>
        /// The credential field containing data.
        /// </summary>
        public string? FromField { get; set; }

        /// <summary>
        /// True if data is required (will throw), false otherwise (will be ignored).
        /// </summary>
        public bool IsDataRequired { get; set; }
    }
}
