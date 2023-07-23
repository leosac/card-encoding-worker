namespace Leosac.CredentialProvisioning.Encoding.Services
{
    public class PrepareAccessControlDataServiceProperties : AccessControlServiceProperties
    {
        public PrepareAccessControlDataServiceProperties()
        {
            BufferBehavior = EncodingServiceBufferBehavior.Overwrite;
        }
    }
}
