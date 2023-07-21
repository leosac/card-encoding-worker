using Leosac.CredentialProvisioning.Core.Models;

namespace Leosac.CredentialProvisioning.Encoding.LLA
{
    public class LLACardContext : CardContext
    {
        public LLACardContext(EncodingDeviceContext deviceContext, CredentialBase? credential = null) : base(deviceContext, credential) { }

        public LLADeviceContext LLADeviceContext
        {
            get => DeviceContext as LLADeviceContext;
        }

        public LibLogicalAccess.Chip? Chip { get; set; }
    }
}
