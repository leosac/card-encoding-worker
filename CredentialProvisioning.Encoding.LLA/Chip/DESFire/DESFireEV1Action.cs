using Leosac.CredentialProvisioning.Core.Contexts;
using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public abstract class DESFireEV1Action<T> : EncodingAction<T> where T : Leosac.CredentialProvisioning.Encoding.Chip.DESFire.DESFireActionProperties, new()
    {
        public override void Run(CredentialContext<EncodingFragmentTemplateContent> encodingCtx, CardContext cardCtx)
        {
            var llaCtx = cardCtx as LLACardContext;
            if (llaCtx == null)
            {
                throw new EncodingException("Device context must be of type LLA");
            }

            var chip = llaCtx.Chip as DESFireEV1Chip;
            if (chip == null)
            {
                throw new EncodingException("Wrong chip type");
            }

            RunDESFireEV1(chip.getDESFireEV1Commands(), encodingCtx as EncodingContext, llaCtx);
        }

        public abstract void RunDESFireEV1(DESFireEV1Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx);
    }
}