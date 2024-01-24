using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class ChangeKeySettings : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ChangeKeySettings>
    {
        public ChangeKeySettings(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ChangeKeySettings properties) : base(properties)
        {
        }

        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.changeKeySettings((DESFireKeySettings)Properties.KeySettings);
        }
    }
}
