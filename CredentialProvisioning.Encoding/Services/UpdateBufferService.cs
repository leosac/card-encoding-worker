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
    }
}
