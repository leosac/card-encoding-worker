using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateLinearRecordFile(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateLinearRecordFile properties) : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateLinearRecordFile>(properties)
    {
        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.createLinearRecordFile(Properties.FileNo, (EncryptionMode)Properties.EncryptionMode, Properties.AccessRights.ConvertForLLA(), Properties.RecordSize, Properties.MaxNumberOfRecords);
        }
    }
}
