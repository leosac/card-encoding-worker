using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class SetConfigurationEV2(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.SetConfigurationEV2 properties) : DESFireEV2Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.SetConfigurationEV2>(properties)
    {
        public override void Run(DESFireEV2Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.setConfiguration(Properties.FormatCardEnabled, Properties.RandomIdEnabled, Properties.PCMandatoryEnabled, Properties.AuthVCMandatoryEnabled);
        }
    }
}
