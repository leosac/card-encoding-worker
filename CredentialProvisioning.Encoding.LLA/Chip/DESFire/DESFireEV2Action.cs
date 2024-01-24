using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public abstract class DESFireEV2Action<T> : LLAAction<T, DESFireEV2Chip> where T : Leosac.CredentialProvisioning.Encoding.Chip.DESFire.DESFireActionProperties, new()
    {
        protected DESFireEV2Action(T properties) : base(properties)
        {

        }

        public override void Run(DESFireEV2Chip chip, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            Run(chip.getDESFireEV2Commands(), encodingCtx, cardCtx);
        }

        public abstract void Run(DESFireEV2Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx);
    }
}