using Leosac.CredentialProvisioning.Core.Contexts;
using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Encoding.Key;
using Leosac.CredentialProvisioning.Encoding.LLA;
using Leosac.CredentialProvisioning.Worker;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    public class EncodingWorker : WorkerBase<EncodingFragmentTemplateContent>
    {
        KeyProvider? _keystore;

        public EncodingWorker(KeyProvider? keystore = null) : base()
        {
            _keystore = keystore;
        }

        protected override CredentialContext<EncodingFragmentTemplateContent> CreateCredentialContext(string templateId, EncodingFragmentTemplateContent template, IList<WorkerCredentialBase> credentials)
        {
            return new EncodingContext() { TemplateId = templateId, TemplateContent = template, Keys = _keystore, Credentials = credentials.ToArray() };
        }

        protected override CredentialProcess<EncodingFragmentTemplateContent> CreateProcess()
        {
            return new EncodingProcess(typeof(LLADeviceContext).Assembly);
        }
    }
}
