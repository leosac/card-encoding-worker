namespace Leosac.CredentialProvisioning.Encoding.Services
{
    /// <summary>
    /// The base access control data encoding service properties.
    /// </summary>
    public abstract class AccessControlDataService : EncodingServiceProperties
    {
        /// <summary>
        /// The access control format to use.
        /// </summary>
        public string Format { get; set; }
    }
}
