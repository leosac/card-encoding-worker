using Leosac.CredentialProvisioning.Core.Contexts;

namespace Leosac.CredentialProvisioning.Encoding.Worker.LLA
{
    public class LLADeviceContext : DeviceContext
    {
        public LibLogicalAccess.ReaderUnit? ReaderUnit { get; set; }

        public LibLogicalAccess.Chip? Chip { get; set; }
    }
}
