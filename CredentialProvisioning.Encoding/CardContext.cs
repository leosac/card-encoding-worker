using Leosac.CredentialProvisioning.Core.Models;

namespace Leosac.CredentialProvisioning.Encoding
{
    public abstract class CardContext
    {
        protected CardContext(EncodingDeviceContext deviceContext, CredentialBase? credential = null)
        {
            DeviceContext = deviceContext;
            Credential = credential;
        }

        public EncodingDeviceContext DeviceContext { get; private set; }

        public CredentialBase? Credential { get; private set; }
    }
}
