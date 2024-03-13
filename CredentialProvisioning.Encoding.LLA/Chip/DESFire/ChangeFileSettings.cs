using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class ChangeFileSettings(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ChangeFileSettings properties) : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ChangeFileSettings>(properties)
    {
        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.changeFileSettings(Properties.FileNo, (LibLogicalAccess.Card.EncryptionMode)Properties.EncryptionMode, Properties.AccessRights.ConvertForLLA(), Properties.Plain);
        }
    }
}
