using Leosac.CredentialProvisioning.Core.Crypto;
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

            if (cardCtx.Buffer == null)
            {
                throw new EncodingException("No data input for the cryptographic operation.");
            }

            byte[]? iv = null;
            if (!string.IsNullOrEmpty(Properties.InitializationVectorField))
            {
                var fieldName = GetCredentialFieldName(Properties.InitializationVectorField);
                var v = cardCtx.GetFieldValue(fieldName);
                if (v != null)
                {
                    if (v is byte[] bv)
                    {
                        iv = bv;
                    }
                    else
                    {
                        iv = Convert.FromHexString(v.ToString()!);
                    }
                }
            }

            var data = Properties.Crypto.Run(key, cardCtx.Buffer, iv);
            HandleBuffer(cardCtx, data);
        }
    }
}
