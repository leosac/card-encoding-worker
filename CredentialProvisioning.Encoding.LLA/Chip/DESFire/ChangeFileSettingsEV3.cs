using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class ChangeFileSettingsEV3(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ChangeFileSettingsEV3 properties) : DESFireEV3Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ChangeFileSettingsEV3>(properties)
    {
        public override void Run(DESFireEV3Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            if (Properties.AccessRights == null || Properties.AccessRights.Length == 0)
            {
                throw new EncodingException("At least one file access rights is expected.");
            }

            cmd.changeFileSettings(Properties.FileNo, (LibLogicalAccess.Card.EncryptionMode)Properties.EncryptionMode, Properties.AccessRights.ConvertForLLA(), Properties.SdmAndMirroring, Properties.TmcLimit, Properties.SdmVcuid, Properties.SdmReadCtr, Properties.SdmReadCtrLimit, Properties.SdmEncFileData, Properties.SdmAsciiEncoding, Properties.SdmAccessRights.ConvertForLLA(), Properties.SdmVcuidOffset, Properties.SdmReadCtrOffset, Properties.SdmPiccDataOffset, Properties.SdmMacInputOffset, Properties.SdmEncOffset, Properties.SdmEncLength, Properties.SdmMacOffset, Properties.SdmReadCtrLimitValue);
        }
    }
}
