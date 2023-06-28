using Leosac.CredentialProvisioning.Core.Contexts;
using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Encoding.LLA;
using Leosac.CredentialProvisioning.Worker;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    public class EncodingWorker : WorkerBase<EncodingFragmentTemplateContent>
    {
        public EncodingWorker() : base()
        {
        }

        protected override CredentialContext<EncodingFragmentTemplateContent> CreateCredentialContext(Guid templateId, EncodingFragmentTemplateContent template, CredentialBase credential)
        {
            return new EncodingContext() { TemplateId = templateId, TemplateContent = template, Credential = credential };
        }

        protected override CredentialProcess<EncodingFragmentTemplateContent> CreateProcess()
        {
            return new EncodingProcess(typeof(LLADeviceContext).Assembly);
        }
    }
}
