using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class Authenticate(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.Authenticate properties) : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.Authenticate>(properties)
    {
        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            if (Properties.Key == null)
            {
                throw new EncodingException("A key must be defined.");
            }
            var key = (encodingCtx.Keys?.Get(Properties.Key, cardCtx.Credential?.VolatileKeys)) ?? throw new EncodingException("Cannot resolve the internal key reference.");
            if (key.CreateKey(cardCtx, Properties.Key?.Diversification) is not DESFireKey desfireKey)
            {
                throw new EncodingException("The key must be of type DESFire.");
            }

            cmd.authenticate(Properties.KeyNo, desfireKey);
        }
    }
}
