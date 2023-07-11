using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Encoding.LLA;
using Leosac.CredentialProvisioning.Encoding.Worker.Contracts;
using Leosac.CredentialProvisioning.Encoding.Worker.LLA;
using Leosac.CredentialProvisioning.Worker;
using LibLogicalAccess.Card;
using LibLogicalAccess.Reader;
using Microsoft.AspNetCore.SignalR;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    public class ReaderHub : Hub<IReaderClient>, IReaderHub
    {
        EncodingWorker _worker;

        public ReaderHub(EncodingWorker worker)
        {
            _worker = worker;
        }

        public Task<string> EncodeFromQueue(Guid templateId, string itemId)
        {
            return Encode(_worker.InitializeProcess(itemId));
        }

        public Task<string> Encode(Guid templateId, CredentialBase credential)
        {
            return Encode(_worker.InitializeProcess(templateId, credential));
        }

        public Task<string> EncodeAll(Guid templateId, CredentialBase[] credentials)
        {
            return Encode(_worker.InitializeProcess(templateId, credentials));
        }

        private Task<string> Encode(CredentialProcess<EncodingFragmentTemplateContent> process)
        {
            var caller = Clients.Caller;
            var clDevice = new LLADeviceContext();
            clDevice.ReaderUnit = new WorkerReaderUnit(caller, string.Empty);

            if (process.CredentialContext != null && process.CredentialContext.TemplateContent?.SAM != null)
            {
                var samDevice = new LLADeviceContext();
                samDevice.ReaderUnit = new WorkerReaderUnit(caller, "SAM");

                clDevice.ReaderUnit.setSAMReaderUnit(samDevice.ReaderUnit);
                if (clDevice.ReaderUnit.getConfiguration() is ISO7816ReaderUnitConfiguration isoConfig)
                {
                    isoConfig.setCheckSAMReaderIsAvailable(false);
                    isoConfig.setAutoConnectToSAMReader(true);
                    isoConfig.setSAMUnlockKey(process.CredentialContext.TemplateContent.SAM.UnlockKey.CreateKey() as DESFireKey, process.CredentialContext.TemplateContent.SAM.UnlockKeyNo);
                    clDevice.ReaderUnit.setConfiguration(isoConfig);
                }
            }
            process.ProcessCompleted += async (sender, state) =>
            {
                await caller.NotifyProcessCompleted(process.Id, state);
            };
            process.Run(clDevice);
            return Task.FromResult(process.Id);
        }
    }
}
