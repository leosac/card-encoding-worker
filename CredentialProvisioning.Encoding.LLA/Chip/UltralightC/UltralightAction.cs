using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.UltralightC
{
    public abstract class UltralightAction<T> : LLAAction<T, MifareUltralightChip> where T : Leosac.CredentialProvisioning.Encoding.Chip.UltralightC.UltralightCActionProperties, new()
    {
        protected UltralightAction(T properties) : base(properties)
        {

        }

        public override void Run(MifareUltralightChip chip, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            Run(chip.getMifareUltralightCommands(), encodingCtx, cardCtx);
        }

        public abstract void Run(MifareUltralightCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx);
    }
}