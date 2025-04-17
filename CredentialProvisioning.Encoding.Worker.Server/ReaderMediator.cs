using Leosac.CredentialProvisioning.Core.Contexts;
using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Encoding.LLA;
using Leosac.CredentialProvisioning.Encoding.Worker.Contracts;
using Leosac.CredentialProvisioning.Server.Shared;
using Leosac.CredentialProvisioning.Worker;
using LibLogicalAccess.Card;
using LibLogicalAccess.Reader;
using System.Diagnostics;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    public class ReaderMediator(ILogger<RemoteReaderHub> logger, EncodingWorker worker, WorkerCredentialDataIntegrity integrity)
    {
        protected readonly ILogger<RemoteReaderHub> _logger = logger;
        protected readonly EncodingWorker _worker = worker;
        protected readonly WorkerCredentialDataIntegrity _integrity = integrity; 

        public Task<string?> EncodeFromQueue(string templateId, string itemId, Func<CredentialProcess<EncodingFragmentTemplateContent>?, DeviceTarget> initializeDevices)
        {
            return Encode(_worker.InitializeProcess(itemId), initializeDevices);
        }

        public Task<string?> Encode(string templateId, WorkerCredentialBase credential, Func<CredentialProcess<EncodingFragmentTemplateContent>?, DeviceTarget> initializeDevices)
        {
            if (_integrity.IsEnabled() && !_integrity.Verify(credential))
            {
                throw new Exception("Invalid credential signature.");
            }
            return Encode(_worker.InitializeProcess(templateId, credential), initializeDevices);
        }

        public Task<string?> EncodeAll(string templateId, WorkerCredentialBase[] credentials, Func<CredentialProcess<EncodingFragmentTemplateContent>?, DeviceTarget> initializeDevices)
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
            return Encode(_worker.InitializeProcess(templateId, credentials), initializeDevices);
        }

        private Task<string?> Encode(CredentialProcess<EncodingFragmentTemplateContent>? process, Func<CredentialProcess<EncodingFragmentTemplateContent>?, DeviceTarget> initializeDevices)
        {
            if (process == null)
            {
                return Task.FromResult<string?>(null);
            }

            var t = initializeDevices(process);
            if (t.ContactlessDevice == null)
            {
                return Task.FromResult<string?>(null);
            }

            if (t.SAMDevice != null)
            {
                t.ContactlessDevice.ReaderUnit?.setSAMReaderUnit(t.SAMDevice.ReaderUnit);
                if (t.ContactlessDevice.ReaderUnit?.getConfiguration() is ISO7816ReaderUnitConfiguration isoConfig)
                {
                    isoConfig.setCheckSAMReaderIsAvailable(false);
                    isoConfig.setAutoConnectToSAMReader(true);
                    isoConfig.setSAMType(process.CredentialContext?.TemplateContent?.SAM?.SAMType ?? "SAM_AUTO");
                    if (!string.IsNullOrEmpty(process.CredentialContext?.TemplateContent?.SAM?.UnlockKey?.KeyId))
                    {
                        var unlockkey = (worker.KeyStore?.Get(process.CredentialContext.TemplateContent.SAM.UnlockKey)) ?? throw new EncodingException("Cannot resolve the internal unlock key reference.");
                        isoConfig.setSAMUnlockKey(unlockkey.CreateKey() as DESFireKey, process.CredentialContext.TemplateContent.SAM.UnlockKeyNo);
                        isoConfig.setUseSAMAuthenticateHost(process.CredentialContext.TemplateContent.SAM.AuthenticationMode == EncodingFragmentTemplateContent.SAMProperties.SAMAuthenticationMode.AuthenticateHost);
                    }
                    t.ContactlessDevice.ReaderUnit.setConfiguration(isoConfig);
                }
            }

            process.ProcessCompleted += async (sender, state) =>
            {
                if (t.Notification != null)
                {
                    await t.Notification.NotifyProcessCompleted(process.Id, state);
                }
            };
            process.Run(t.ContactlessDevice).ContinueWith(failedTask =>
            {
                _logger.LogInformation("Encoding Process failure: {Message}", failedTask.Exception?.Message);
                t.Notification?.NotifyError(process.Id, failedTask.Exception?.Message);
            }, TaskContinuationOptions.OnlyOnFaulted);
            return Task.FromResult<string?>(process.Id);
        }
    }
}
