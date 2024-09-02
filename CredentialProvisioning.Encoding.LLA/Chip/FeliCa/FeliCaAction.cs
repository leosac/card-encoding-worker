using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.FeliCa
{
    public abstract class FeliCaAction<T> : LLAAction<T, FeliCaChip> where T : Leosac.CredentialProvisioning.Encoding.Chip.FeliCa.FeliCaActionProperties, new ()
    {
        protected FeliCaAction(T properties) : base(properties)
        {

        }

        public override void Run(FeliCaChip chip, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            Run(chip.getFeliCaCommands(), encodingCtx, cardCtx);
        }

        public abstract void Run(FeliCaCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx);
    }
}
