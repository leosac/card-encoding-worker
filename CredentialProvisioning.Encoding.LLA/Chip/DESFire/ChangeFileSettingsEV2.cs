using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class ChangeFileSettingsEV2(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ChangeFileSettingsEV2 properties) : DESFireEV2Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ChangeFileSettingsEV2>(properties)
    {
        public override void Run(DESFireEV2Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            if (Properties.AccessRights == null || Properties.AccessRights.Length == 0)
            {
                throw new EncodingException("At least one file access rights is expected.");
            }

            cmd.changeFileSettings(Properties.FileNo, (LibLogicalAccess.Card.EncryptionMode)Properties.EncryptionMode, Properties.AccessRights.ConvertForLLA());
        }
    }
}
