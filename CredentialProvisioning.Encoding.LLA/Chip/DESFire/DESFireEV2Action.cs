using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public abstract class DESFireEV2Action<T>(T properties) : LLAAction<T, DESFireEV2Chip>(properties) where T : Leosac.CredentialProvisioning.Encoding.Chip.DESFire.DESFireActionProperties, new()
    {
        public override void Run(DESFireEV2Chip chip, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            Run(chip.getDESFireEV2Commands(), encodingCtx, cardCtx);
        }

        public abstract void Run(DESFireEV2Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx);
    }
}