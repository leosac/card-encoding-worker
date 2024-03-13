using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateStdDataFileEV1(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateStdDataFileEV1 properties) : DESFireEV1Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateStdDataFileEV1>(properties)
    {
        public override void Run(DESFireEV1Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            if (Properties.IsoFID != null)
                cmd.createStdDataFile(Properties.FileNo, (EncryptionMode)Properties.EncryptionMode, Properties.AccessRights.ConvertForLLA(), Properties.FileSize, Properties.IsoFID.Value);
            else
                cmd.createStdDataFile(Properties.FileNo, (EncryptionMode)Properties.EncryptionMode, Properties.AccessRights.ConvertForLLA(), Properties.FileSize);
        }
    }
}
