using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateBackupFile(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateBackupFile properties) : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateBackupFile>(properties)
    {
        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.createBackupFile(Properties.FileNo, (EncryptionMode)Properties.EncryptionMode, Properties.AccessRights.ConvertForLLA(), Properties.FileSize);
        }
    }
}
