using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class ChangeFileSettings : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ChangeFileSettings>
    {
        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.changeFileSettings(Properties.FileNo, (LibLogicalAccess.Card.EncryptionMode)Properties.EncryptionMode, Properties.AccessRights.ConvertForLLA(), Properties.Plain);
        }
    }
}
