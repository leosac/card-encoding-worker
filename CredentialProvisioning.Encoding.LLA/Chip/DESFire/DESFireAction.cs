using Leosac.CredentialProvisioning.Core.Contexts;
using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public abstract class DESFireAction<T>(T properties) : EncodingAction<T>(properties) where T : Leosac.CredentialProvisioning.Encoding.Chip.DESFire.DESFireActionProperties, new()
    {
        public override void Run(CredentialContext<EncodingFragmentTemplateContent> encodingCtx, CardContext cardCtx)
        {
            if (cardCtx is not LLACardContext llaCtx)
            {
                throw new EncodingException("Device context must be of type LLA");
            }
            if (encodingCtx is not EncodingContext encCtx)
            {
                throw new EncodingException("Encoding context must be of type EncodingContext");
            }

            var chip = llaCtx.Chip as DESFireChip ?? throw new EncodingException("Wrong chip type");
            Run(chip.getDESFireCommands(), encCtx, llaCtx);
        }

        public abstract void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx);
    }
}