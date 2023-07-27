using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateCyclicRecordFile : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateCyclicRecordFile>
    {
        public CreateCyclicRecordFile(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateCyclicRecordFile properties) : base(properties)
        {

        }

        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.createCyclicRecordFile(Properties.FileNo, (EncryptionMode)Properties.EncryptionMode, Properties.AccessRights.ConvertForLLA(), Properties.RecordSize, Properties.MaxNumberOfRecords);
        }
    }
}
