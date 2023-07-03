namespace Leosac.CredentialProvisioning.Encoding
{
    public abstract class CardContext
    {
        protected CardContext(EncodingDeviceContext deviceContext)
        {
            DeviceContext = deviceContext;
        }

        public EncodingDeviceContext DeviceContext { get; private set; }
    }
}
