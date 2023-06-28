using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateLinearRecordFile : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateLinearRecordFile>
    {
        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLADeviceContext deviceCtx)
        {
            cmd.createLinearRecordFile(Properties.FileNo, (EncryptionMode)Properties.EncryptionMode, Properties.AccessRights.ConvertForLLA(), Properties.RecordSize, Properties.MaxNumberOfRecords);
        }
    }
}
