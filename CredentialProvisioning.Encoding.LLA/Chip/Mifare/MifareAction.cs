using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.Mifare
{
    public abstract class MifareAction<T> : LLAAction<T, MifareChip> where T : Leosac.CredentialProvisioning.Encoding.Chip.Mifare.MifareActionProperties, new()
    {
        protected MifareAction(T properties) : base(properties)
        {

        }

        public override void Run(MifareChip chip, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            Run(chip.getMifareCommands(), encodingCtx, cardCtx);
        }

        public abstract void Run(MifareCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx);
    }
}