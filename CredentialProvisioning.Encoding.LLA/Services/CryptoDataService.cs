using Leosac.CredentialProvisioning.Core.Crypto;
using Leosac.CredentialProvisioning.Encoding.Key;
    
namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public class CryptoDataService(Encoding.Services.CryptoDataService properties) : EncodingService<Encoding.Services.CryptoDataService>(properties)
    {
        public override void Run(CardContext cardCtx, KeyProvider? keystore, EncodingAction currentAction)
        {
            var key = (keystore?.Get(Properties.Key, cardCtx.Credential?.VolatileKeys)) ?? throw new EncodingException("Cannot resolve the key for the crypto operation.");
            if (cardCtx.Buffer == null)
            {
                throw new EncodingException("No data input for the cryptographic operation.");
            }

            byte[]? iv = null;
            if (!string.IsNullOrEmpty(Properties.InitializationVectorField))
            {
                var fieldName = GetCredentialFieldName(Properties.InitializationVectorField);
                iv = cardCtx.GetBinaryFieldValue(fieldName);
            }

            var data = Properties.Crypto.Run(key, cardCtx.Buffer, iv);
            HandleBuffer(cardCtx, data);
        }
    }
}
