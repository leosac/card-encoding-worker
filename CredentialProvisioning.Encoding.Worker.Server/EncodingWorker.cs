using Leosac.CredentialProvisioning.Core.Contexts;
using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Encoding.Key;
using Leosac.CredentialProvisioning.Encoding.LLA;
using Leosac.CredentialProvisioning.Worker;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    public class EncodingWorker(ILogger<EncodingWorker> logger, KeyProvider keystore) : WorkerBase<EncodingFragmentTemplateContent>()
    {
        protected readonly ILogger<EncodingWorker> _logger = logger;

        public KeyProvider KeyStore { get; private set; } = keystore;

        protected override CredentialContext<EncodingFragmentTemplateContent> CreateCredentialContext(string templateId, EncodingFragmentTemplateContent template, IList<WorkerCredentialBase> credentials)
        {
            return new EncodingContext() { TemplateId = templateId, TemplateContent = template, Keys = KeyStore, Credentials = [.. credentials] };
        }

        protected override CredentialProcess<EncodingFragmentTemplateContent> CreateProcess()
        {
            return new EncodingProcess(typeof(LLADeviceContext).Assembly, _logger);
        }
    }
}
