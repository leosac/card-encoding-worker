using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateStdDataFileEV2(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateStdDataFileEV2 properties) : DESFireEV2Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateStdDataFileEV2>(properties)
    {
        public override void Run(DESFireEV2Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.createStdDataFile(Properties.FileNo, (EncryptionMode)Properties.EncryptionMode, Properties.AccessRights.ConvertForLLA(), Properties.FileSize, Properties.IsoFID ?? 0, Properties.MultiAccessRights);
        }
    }
}
