using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Encoding.LLA;
using Leosac.CredentialProvisioning.Encoding.Worker.Contracts;
using Leosac.CredentialProvisioning.Encoding.Worker.LLA;
using Leosac.CredentialProvisioning.Worker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    public class RemoteReaderHub(ReaderMediator readerMediator, IOptions<Options> options) : Hub<IReaderClient>, IReaderHub
    {
        protected readonly ReaderMediator _readerMediator = readerMediator;
        protected readonly IOptions<Options> _options = options;

        public const string API_VERSION = "1.1.0";

        public Task<string> GetAPIVersion()
        {
            return Task.FromResult(API_VERSION);
        }

        [Authorize("queue")]
        public Task<string?> EncodeFromQueue(string templateId, string itemId, bool waitRemoval = true)
        {
            return _readerMediator.EncodeFromQueue(templateId, itemId, (process) => Initialize(process, waitRemoval));
        }

        [Authorize]
        public Task<string?> Encode(string templateId, WorkerCredentialBase credential, bool waitRemoval = true)
        {
            return _readerMediator.Encode(templateId, credential, (process) => Initialize(process, waitRemoval));
        }

        protected DeviceTarget Initialize(CredentialProcess<EncodingFragmentTemplateContent>? process, bool waitRemoval)
        {
            string? apiVersion = null;
            if (Context.Items.ContainsKey("API_VERSION"))
            {
                apiVersion = Context.Items["API_VERSION"]?.ToString();
            }
            if (string.IsNullOrEmpty(apiVersion))
            {
                apiVersion = "1.0.0";
            }

            var t = new DeviceTarget();
            var caller = Clients.Caller;
            t.ContactlessDevice = new LLADeviceContext
            {
                ReaderUnit = new WorkerRemoteReaderUnit(caller, apiVersion, _options.Value.ContactlessReader, waitRemoval)
            };

            if (process?.CredentialContext?.TemplateContent?.SAM != null)
            {
                t.SAMDevice = new LLADeviceContext
                {
                    ReaderUnit = new WorkerRemoteReaderUnit(caller, apiVersion, _options.Value.SAMReader)
                };
            }
            t.Notification = caller;
            return t;
        }
    }
}
