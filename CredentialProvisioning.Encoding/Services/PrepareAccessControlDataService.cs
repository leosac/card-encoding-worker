namespace Leosac.CredentialProvisioning.Encoding.Services
{
    public class PrepareAccessControlDataService : AccessControlDataService
    {
        public PrepareAccessControlDataService()
        {
            BufferBehavior = EncodingServiceBufferBehavior.Overwrite;
        }
    }
}
