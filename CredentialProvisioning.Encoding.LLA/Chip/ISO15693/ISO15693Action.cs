using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.ISO15693
{
    public abstract class ISO15693Action<T> : LLAAction<T, ISO15693Chip> where T : Leosac.CredentialProvisioning.Encoding.Chip.ISO15693.ISO15693ActionProperties, new()
    {
        protected ISO15693Action(T properties) : base(properties)
        {

        }

        public override void Run(ISO15693Chip chip, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            Run(chip.getISO15693Commands(), encodingCtx, cardCtx);
        }

        public abstract void Run(ISO15693Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx);
    }
}
