using Leosac.CredentialProvisioning.Core.Contexts;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip
{
    public class Nothing : EncodingAction<Leosac.CredentialProvisioning.Encoding.Chip.Nothing>
    {
        public Nothing(Leosac.CredentialProvisioning.Encoding.Chip.Nothing properties) : base(properties)
        {

        }

        public override void Run(CredentialContext<EncodingFragmentTemplateContent> encodingCtx, CardContext cardCtx)
        {
            // Do nothing
        }
    }
}
