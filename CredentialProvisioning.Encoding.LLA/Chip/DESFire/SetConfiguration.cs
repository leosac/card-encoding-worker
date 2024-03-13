using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class SetConfiguration(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.SetConfiguration properties) : DESFireEV1Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.SetConfiguration>(properties)
    {
        public override void Run(DESFireEV1Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            if (Properties.DefaultKey != null)
            {
                var key = (encodingCtx.Keys?.Get(Properties.DefaultKey, cardCtx.Credential?.VolatileKeys)) ?? throw new EncodingException("Cannot resolve the internal key reference.");
                var desfireKey = key.CreateKey(cardCtx, Properties.DefaultKey?.Diversification) as DESFireKey ?? throw new EncodingException("The key must be of type DESFire.");
                cmd.setConfiguration(desfireKey);
            }
            else
            {
                cmd.setConfiguration(Properties.FormatCardEnabled, Properties.RandomIdEnabled);
            }
        }
    }
}
