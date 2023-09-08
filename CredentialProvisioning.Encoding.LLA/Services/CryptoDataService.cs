using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public class CryptoDataService : EncodingService<Encoding.Services.CryptoDataService>
    {
        public CryptoDataService(Encoding.Services.CryptoDataService properties) : base(properties)
        {

        }

        public override void Run(CardContext cardCtx, KeyProvider? keystore, EncodingAction currentAction)
        {
            var key = keystore?.Get(Properties.Key, cardCtx.Credential?.VolatileKeys);
            if (key == null)
            {
                throw new EncodingException("Cannot resolve the key for the crypto operation.");
            }

            // TODO: implements this
        }
    }
}
