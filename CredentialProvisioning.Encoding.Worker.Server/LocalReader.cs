using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Encoding.Worker.Contracts;
using Leosac.CredentialProvisioning.Worker;
using LibLogicalAccess.Reader;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    public class LocalReader(IOptions<Options> options) : IProductionNotification
    {
        protected readonly IOptions<Options> _options = options;

        public Task NotifyError(string processId, string? message)
        {
            Console.Error.WriteLine("Error on Encoding Process `{0}`: {1}", processId, message);
            return Task.CompletedTask;
        }

        public Task NotifyProcessCompleted(string processId, ProvisioningState state)
        {
            Console.Error.WriteLine("Encoding Process `{0}` completed with state: {1}", processId, state);
            return Task.CompletedTask;
        }

        public DeviceTarget Initialize(CredentialProcess<EncodingFragmentTemplateContent>? process)
        {
            var t = new DeviceTarget();
            var rp = PCSCReaderProvider.createInstance();
            var connectedReaders = rp.getReaderList();
            t.ContactlessDevice = new Encoding.LLA.LLADeviceContext
            {
                ReaderUnit = (string.IsNullOrEmpty(options.Value.ContactlessReader) ? rp.createReaderUnit() : connectedReaders.FirstOrDefault(r => r.getName() == options.Value.ContactlessReader)) as PCSCReaderUnit
            };
            if (t.ContactlessDevice.ReaderUnit == null)
            {
                Console.Error.WriteLine("The expected contactless PC/SC reader wasn't found.");
            }
            if (!string.IsNullOrEmpty(options.Value.SAMReader))
            {
                t.SAMDevice = new Encoding.LLA.LLADeviceContext
                {
                    ReaderUnit = connectedReaders.FirstOrDefault(r => r.getName() == options.Value.ContactlessReader) as PCSCReaderUnit
                };
                if (t.SAMDevice.ReaderUnit == null)
                {
                    Console.Error.WriteLine("The expected SAM PC/SC reader wasn't found.");
                }
            }
            t.Notification = this;
            return t;
        }
    }
}
