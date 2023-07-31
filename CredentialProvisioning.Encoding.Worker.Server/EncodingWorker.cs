using Leosac.CredentialProvisioning.Core.Contexts;
using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Encoding.Key;
using Leosac.CredentialProvisioning.Encoding.LLA;
using Leosac.CredentialProvisioning.Worker;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    public class EncodingWorker : WorkerBase<EncodingFragmentTemplateContent>
    {
        ILogger<EncodingWorker> _logger;
        KeyProvider? _keystore;

        public EncodingWorker(ILogger<EncodingWorker> logger, KeyProvider keystore) : base()
        {
            _keystore = keystore;
            _logger = logger;
        }

        protected override CredentialContext<EncodingFragmentTemplateContent> CreateCredentialContext(string templateId, EncodingFragmentTemplateContent template, IList<WorkerCredentialBase> credentials)
        {
            return new EncodingContext() { TemplateId = templateId, TemplateContent = template, Keys = _keystore, Credentials = credentials.ToArray() };
        }

        protected override CredentialProcess<EncodingFragmentTemplateContent> CreateProcess()
        {
            return new EncodingProcess(typeof(LLADeviceContext).Assembly, _logger);
        }
    }
}
