using Leosac.CredentialProvisioning.Core.Contexts;
using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip
{
    public abstract class LLAAction<T, C> : EncodingAction<T> where T : Leosac.CredentialProvisioning.Encoding.Chip.DESFire.DESFireActionProperties, new() where C : LibLogicalAccess.Chip
    {
        protected LLAAction(T properties) : base(properties)
        {

        }

        public override void Run(CredentialContext<EncodingFragmentTemplateContent> encodingCtx, CardContext cardCtx)
        {
            var llaCtx = cardCtx as LLACardContext;
            if (llaCtx == null)
            {
                throw new EncodingException("Device context must be of type LLA");
            }

            var chip = llaCtx.Chip as C;
            if (chip == null)
            {
                throw new EncodingException("Wrong chip type");
            }

            Run(chip, encodingCtx as EncodingContext, llaCtx);
        }

        public abstract void Run(C chip, EncodingContext encodingCtx, LLACardContext cardCtx);
    }
}
