using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.UltralightC
{
    public abstract class UltralightCAction<T> : LLAAction<T, MifareUltralightCChip> where T : Leosac.CredentialProvisioning.Encoding.Chip.UltralightC.UltralightCActionProperties, new()
    {
        protected UltralightCAction(T properties) : base(properties)
        {

        }

        public override void Run(MifareUltralightCChip chip, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            Run(chip.getMifareUltralightCCommands(), encodingCtx, cardCtx);
        }

        public abstract void Run(MifareUltralightCCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx);
    }
}