namespace Leosac.CredentialProvisioning.Encoding.Services
{
    public class PrepareAccessControlDataServiceProperties : AccessControlDataServiceProperties
    {
        public PrepareAccessControlDataServiceProperties()
        {
            BufferBehavior = EncodingServiceBufferBehavior.Overwrite;
        }
    }
}
