using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateBackupFile : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateBackupFile>
    {
        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLADeviceContext deviceCtx)
        {
            cmd.createBackupFile(Properties.FileNo, (EncryptionMode)Properties.EncryptionMode, Properties.AccessRights.ConvertForLLA(), Properties.FileSize);
        }
    }
}
