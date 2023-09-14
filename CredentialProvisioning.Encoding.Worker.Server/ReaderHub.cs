using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Encoding.LLA;
using Leosac.CredentialProvisioning.Encoding.Worker.Contracts;
using Leosac.CredentialProvisioning.Encoding.Worker.LLA;
using Leosac.CredentialProvisioning.Server.Shared;
using Leosac.CredentialProvisioning.Worker;
using LibLogicalAccess;
using LibLogicalAccess.Card;
using LibLogicalAccess.Reader;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    public class ReaderHub : Hub<IReaderClient>, IReaderHub
    {
        protected readonly ILogger<ReaderHub> _logger;
        protected readonly EncodingWorker _worker;
        protected readonly WorkerCredentialDataIntegrity _integrity;
        protected readonly IOptions<Options> _options;

        public ReaderHub(ILogger<ReaderHub> logger, EncodingWorker worker, WorkerCredentialDataIntegrity integrity, IOptions<Options> options)
        {
            _logger = logger;
            _worker = worker;
            _integrity = integrity;
            _options = options;
        }

        public Task<string> EncodeFromQueue(string templateId, string itemId, bool waitRemoval = true)
        {
            return Encode(_worker.InitializeProcess(itemId), waitRemoval);
        }

        public Task<string> Encode(string templateId, WorkerCredentialBase credential, bool waitRemoval = true)
        {
            if (_integrity.IsEnabled() && !_integrity.Verify(credential))
            {
                throw new Exception("Invalid credential signature.");
            }
            return Encode(_worker.InitializeProcess(templateId, credential), waitRemoval);
        }

        public Task<string> EncodeAll(string templateId, WorkerCredentialBase[] credentials)
        {
            if (_integrity.IsEnabled())
            {
                foreach (var credential in credentials)
                {
                    if (!_integrity.Verify(credential))
                    {
                        throw new Exception("Invalid credential signature.");
                    }
                }
            }
            return Encode(_worker.InitializeProcess(templateId, credentials));
        }

        private Task<string> Encode(CredentialProcess<EncodingFragmentTemplateContent> process, bool waitRemoval = true)
        {
            var caller = Clients.Caller;
            var clDevice = new LLADeviceContext();
            clDevice.ReaderUnit = new WorkerRemoteReaderUnit(caller, _options.Value.ContactlessReader, waitRemoval);

            if (process.CredentialContext != null && process.CredentialContext.TemplateContent?.SAM != null)
            {
                var samDevice = new LLADeviceContext();
                samDevice.ReaderUnit = new WorkerRemoteReaderUnit(caller, _options.Value.SAMReader);

                clDevice.ReaderUnit.setSAMReaderUnit(samDevice.ReaderUnit);
                if (clDevice.ReaderUnit.getConfiguration() is ISO7816ReaderUnitConfiguration isoConfig)
                {
                    isoConfig.setCheckSAMReaderIsAvailable(false);
                    isoConfig.setAutoConnectToSAMReader(true);
                    isoConfig.setSAMType("SAM_AV2"); // TODO: support additional SAM technologies
                    var unlockkey = _worker.KeyStore?.Get(process.CredentialContext.TemplateContent.SAM.UnlockKey);
                    if (unlockkey == null)
                    {
                        throw new EncodingException("Cannot resolve the internal unlock key reference.");
                    }
                    isoConfig.setSAMUnlockKey(unlockkey.CreateKey() as DESFireKey, process.CredentialContext.TemplateContent.SAM.UnlockKeyNo);
                    clDevice.ReaderUnit.setConfiguration(isoConfig);
                }
            }
            process.ProcessCompleted += async (sender, state) =>
            {
                await caller.NotifyProcessCompleted(process.Id, state);
            };
            process.Run(clDevice).ContinueWith(failedTask =>
            {
                _logger.LogInformation(string.Format("Encoding Process failured: {0}", failedTask.Exception?.Message));
                caller.NotifyError(process.Id, failedTask.Exception?.Message);
            }, TaskContinuationOptions.OnlyOnFaulted);
            return Task.FromResult(process.Id);
        }
    }
}
