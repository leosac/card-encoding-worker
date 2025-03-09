using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class ChangeKeySettings(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ChangeKeySettings properties) : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ChangeKeySettings>(properties)
    {
        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.changeKeySettings((DESFireKeySettings)((byte)Properties.KeySettings | (Properties.ChangeKey << 4)));
        }
    }
}
