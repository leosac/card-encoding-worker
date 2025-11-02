namespace Leosac.CredentialProvisioning.Encoding.Services
{
    /// <summary>
    /// Prepare Access Control data encoding service, for further writing.
    /// </summary>
    public class PrepareAccessControlDataService : AccessControlDataService
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PrepareAccessControlDataService()
        {
            BufferBehavior = EncodingServiceBufferBehavior.Overwrite;
        }

        /// <summary>
        /// Check the field value maximum length. Default is true.
        /// </summary>
        public bool? CheckFieldValueMaxLength { get; set; }
    }
}
