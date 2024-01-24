using Leosac.CredentialProvisioning.Core.Contexts;
using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public abstract class DESFireEV1Action<T> : LLAAction<T, DESFireEV1Chip> where T : Leosac.CredentialProvisioning.Encoding.Chip.DESFire.DESFireActionProperties, new()
    {
        protected DESFireEV1Action(T properties) : base(properties)
        {

        }

        public override void Run(DESFireEV1Chip chip, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            Run(chip.getDESFireEV1Commands(), encodingCtx, cardCtx);
        }

        public abstract void Run(DESFireEV1Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx);
    }
}