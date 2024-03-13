using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public abstract class DESFireEV1Action<T>(T properties) : LLAAction<T, DESFireEV1Chip>(properties) where T : Leosac.CredentialProvisioning.Encoding.Chip.DESFire.DESFireActionProperties, new()
    {
        public override void Run(DESFireEV1Chip chip, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            Run(chip.getDESFireEV1Commands(), encodingCtx, cardCtx);
        }

        public abstract void Run(DESFireEV1Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx);
    }
}