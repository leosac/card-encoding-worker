using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class SetConfigurationMessaging : DESFireEV2Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.SetConfigurationMessaging>
    {
        public SetConfigurationMessaging(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.SetConfigurationMessaging properties) : base(properties)
        {

        }

        public override void Run(DESFireEV2Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.setConfiguration(Properties.D40SecureMessagingEnabled, Properties.EV1SecureMessagingEnabled, Properties.EV2ChainedWritingEnabled);
        }
    }
}
