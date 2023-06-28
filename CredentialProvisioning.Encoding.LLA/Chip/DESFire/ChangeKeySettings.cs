using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class ChangeKeySettings : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ChangeKeySettings>
    {
        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLADeviceContext deviceCtx)
        {
            cmd.changeKeySettings((DESFireKeySettings)Properties.KeySettings);
        }
    }
}
