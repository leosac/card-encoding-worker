using Leosac.CredentialProvisioning.Core.Contexts;
using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Encoding.Worker.LLA;
using Leosac.CredentialProvisioning.Worker;
using System.Net;

namespace Leosac.CredentialProvisioning.Encoding.Worker.LLAServer
{
    public class EncodingWorker : ProductionSetWorker<EncodingFragmentTemplateContent>
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
