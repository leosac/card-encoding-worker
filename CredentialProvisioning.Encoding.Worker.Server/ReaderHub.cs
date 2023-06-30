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

        public Task EncodeFromQueue(Guid templateId, Guid itemId)
        {
            return Encode(_worker.InitializeProcess(itemId));
        }

        public Task Encode(Guid templateId, CredentialBase credential)
        {
            return Encode(_worker.InitializeProcess(templateId, credential));
        }

        private Task Encode(CredentialProcess<EncodingFragmentTemplateContent> process)
        {
            var clDevice = new LLADeviceContext();
            clDevice.ReaderUnit = new WorkerReaderUnit(Clients.Caller, string.Empty);

            if (process.CredentialContext != null && process.CredentialContext.TemplateContent?.SAM != null)
            {
                var samDevice = new LLADeviceContext();
                samDevice.ReaderUnit = new WorkerReaderUnit(Clients.Caller, "SAM");

                clDevice.ReaderUnit.setSAMReaderUnit(samDevice.ReaderUnit);
                if (clDevice.ReaderUnit.getConfiguration() is ISO7816ReaderUnitConfiguration isoConfig)
                {
                    isoConfig.setCheckSAMReaderIsAvailable(false);
                    isoConfig.setAutoConnectToSAMReader(true);
                    isoConfig.setSAMUnlockKey(process.CredentialContext.TemplateContent.SAM.UnlockKey.CreateKey() as DESFireKey, process.CredentialContext.TemplateContent.SAM.UnlockKeyNo);
                    clDevice.ReaderUnit.setConfiguration(isoConfig);
                }
            }

            process.Run(clDevice);
            return Task.CompletedTask;
        }
    }
}
