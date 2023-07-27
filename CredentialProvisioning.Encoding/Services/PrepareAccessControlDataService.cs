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
    }
}
