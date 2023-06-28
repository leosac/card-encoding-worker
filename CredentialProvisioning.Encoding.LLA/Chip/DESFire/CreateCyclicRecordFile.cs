using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateCyclicRecordFile : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateCyclicRecordFile>
    {
        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLADeviceContext deviceCtx)
        {
            cmd.createCyclicRecordFile(Properties.FileNo, (EncryptionMode)Properties.EncryptionMode, Properties.AccessRights.ConvertForLLA(), Properties.RecordSize, Properties.MaxNumberOfRecords);
        }
    }
}
