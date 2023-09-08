using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class SetConfiguration : DESFireEV1Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.SetConfiguration>
    {
        public SetConfiguration(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.SetConfiguration properties) : base(properties)
        {

        }

        public override void RunDESFireEV1(DESFireEV1Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            if (Properties.DefaultKey != null)
            {
                var key = encodingCtx.Keys?.Get(Properties.DefaultKey, cardCtx.Credential?.VolatileKeys);
                if (key == null)
                {
                    throw new EncodingException("Cannot resolve the internal key reference.");
                }

                var desfireKey = key.CreateKey(cardCtx, Properties.DefaultKey?.Diversification) as DESFireKey;
                if (desfireKey == null)
                {
                    throw new EncodingException("The key must be of type DESFire.");
                }
                cmd.setConfiguration(desfireKey);
            }
            else
            {
                cmd.setConfiguration(Properties.FormatCardEnabled, Properties.RandomIdEnabled);
            }
        }
    }
}
