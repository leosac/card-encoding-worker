using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateStdDataFile : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateStdDataFile>
    {
        public CreateStdDataFile(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateStdDataFile properties) : base(properties)
        {

        }

        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.createStdDataFile(Properties.FileNo, (EncryptionMode)Properties.EncryptionMode, Properties.AccessRights.ConvertForLLA(), Properties.FileSize);
        }
    }
}
