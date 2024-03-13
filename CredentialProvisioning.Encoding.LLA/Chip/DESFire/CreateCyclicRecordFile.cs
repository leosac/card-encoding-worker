using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateCyclicRecordFile(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateCyclicRecordFile properties) : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateCyclicRecordFile>(properties)
    {
        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.createCyclicRecordFile(Properties.FileNo, (EncryptionMode)Properties.EncryptionMode, Properties.AccessRights.ConvertForLLA(), Properties.RecordSize, Properties.MaxNumberOfRecords);
        }
    }
}
