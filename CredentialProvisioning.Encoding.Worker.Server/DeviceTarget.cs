using Leosac.CredentialProvisioning.Encoding.LLA;
using Leosac.CredentialProvisioning.Encoding.Worker.Contracts;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    public class DeviceTarget
    {
        public IProductionNotification? Notification { get; set; }

        public LLADeviceContext? ContactlessDevice { get; set; }

        public LLADeviceContext? SAMDevice { get; set; }
    }
}
