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

        protected override CredentialContext<EncodingFragmentTemplateContent> CreateCredentialContext(string templateId, EncodingFragmentTemplateContent template, IList<CredentialBase> credentials)
        {
            return new EncodingContext() { TemplateId = templateId, TemplateContent = template, Credentials = credentials.ToArray() };
        }

        protected override CredentialProcess<EncodingFragmentTemplateContent> CreateProcess()
        {
            return new EncodingProcess(typeof(LLADeviceContext).Assembly);
        }
    }
}
