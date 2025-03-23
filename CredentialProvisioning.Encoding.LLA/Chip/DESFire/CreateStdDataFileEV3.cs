using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateStdDataFileEV3(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateStdDataFileEV3 properties) : DESFireEV3Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateStdDataFileEV3>(properties)
    {
        public override void Run(DESFireEV3Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.createStdDataFile(Properties.FileNo, (EncryptionMode)Properties.EncryptionMode, Properties.AccessRights.ConvertForLLA(), Properties.FileSize, Properties.IsoFID ?? 0, Properties.MultiAccessRights, Properties.SDMAndMirroring);
        }
    }
}
