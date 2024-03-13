using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class AuthenticateEV2First(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.AuthenticateEV2First properties) : DESFireEV2Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.AuthenticateEV2First>(properties)
    {
        public override void Run(DESFireEV2Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
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

            cmd.authenticateEV2First(Properties.KeyNo, desfireKey);
        }
    }
}
