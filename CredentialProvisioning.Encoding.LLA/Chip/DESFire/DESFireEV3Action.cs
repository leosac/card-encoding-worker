using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public abstract class DESFireEV3Action<T>(T properties) : LLAAction<T, DESFireEV3Chip>(properties) where T : Leosac.CredentialProvisioning.Encoding.Chip.DESFire.DESFireActionProperties, new()
    {
        public override void Run(DESFireEV3Chip chip, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            Run(chip.getDESFireEV3Commands(), encodingCtx, cardCtx);
        }

        public abstract void Run(DESFireEV3Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx);
    }
}