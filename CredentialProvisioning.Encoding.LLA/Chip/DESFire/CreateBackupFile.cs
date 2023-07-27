using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateBackupFile : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateBackupFile>
    {
        public CreateBackupFile(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateBackupFile properties) : base(properties)
        {

        }

        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.createBackupFile(Properties.FileNo, (EncryptionMode)Properties.EncryptionMode, Properties.AccessRights.ConvertForLLA(), Properties.FileSize);
        }
    }
}
