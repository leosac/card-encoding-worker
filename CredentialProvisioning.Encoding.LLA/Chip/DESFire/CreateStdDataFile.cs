using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateStdDataFile(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateStdDataFile properties) : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateStdDataFile>(properties)
    {
        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            Properties.FileNo = encodingCtx.Resolve(Properties.FileNo);
            cmd.createStdDataFile(Properties.FileNo, (EncryptionMode)Properties.EncryptionMode, Properties.AccessRights.ConvertForLLA(), Properties.FileSize);
        }
    }
}
