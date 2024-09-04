using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Encoding.LLA;
using Leosac.CredentialProvisioning.Encoding.Worker.Contracts;
using Leosac.CredentialProvisioning.Encoding.Worker.LLA;
using Leosac.CredentialProvisioning.Worker;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    public class RemoteReaderHub(ReaderMediator readerMediator, IOptions<Options> options) : Hub<IReaderClient>, IReaderHub
    {
        protected readonly ReaderMediator _readerMediator = readerMediator;
        protected readonly IOptions<Options> _options = options;

        public Task<string?> EncodeFromQueue(string templateId, string itemId, bool waitRemoval = true)
        {
            return _readerMediator.EncodeFromQueue(templateId, itemId, (process) => Initialize(process, waitRemoval));
        }

        public Task<string?> Encode(string templateId, WorkerCredentialBase credential, bool waitRemoval = true)
        {
            return _readerMediator.Encode(templateId, credential, (process) => Initialize(process, waitRemoval));
        }

        protected DeviceTarget Initialize(CredentialProcess<EncodingFragmentTemplateContent>? process, bool waitRemoval)
        {
            var t = new DeviceTarget();
            var caller = Clients.Caller;
            t.ContactlessDevice = new LLADeviceContext
            {
                ReaderUnit = new WorkerRemoteReaderUnit(caller, _options.Value.ContactlessReader, waitRemoval)
            };

            if (process?.CredentialContext?.TemplateContent?.SAM != null)
            {
                t.SAMDevice = new LLADeviceContext
                {
                    ReaderUnit = new WorkerRemoteReaderUnit(caller, _options.Value.SAMReader)
                };
            }
            t.Notification = caller;
            return t;
        }
    }
}
