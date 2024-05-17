using Leosac.CredentialProvisioning.Core.Crypto;
using Leosac.CredentialProvisioning.Encoding.Key;
    
namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public class CryptoDataService(Encoding.Services.CryptoDataService properties) : EncodingService<Encoding.Services.CryptoDataService>(properties)
    {
        public override void Run(CardContext cardCtx, KeyProvider? keystore, EncodingFragmentTemplateContent.FragmentTemplateProperty[]? templateProperties, EncodingAction currentAction)
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

            byte[] data;
            if (Properties.Crypto.Operation == Core.Models.CryptoOperation.Sign && key.KeyType == "aes128" && !string.IsNullOrEmpty(key.Value))
            {
                if (iv != null)
                {
                    var niv = new byte[16];
                    Array.Copy(iv, niv, iv.Length);
                    if (iv.Length < niv.Length)
                    {
                        niv[iv.Length] = 0x80;
                    }
                    iv = niv;
                }
                var cdata = LibLogicalAccess.Crypto.CMACCrypto.cmac(new LibLogicalAccess.ByteVector(Convert.FromHexString(key.Value)), "aes", new LibLogicalAccess.ByteVector(cardCtx.Buffer), new LibLogicalAccess.ByteVector(iv)).ToArray();
                // Get the first 8 bytes only
                data = new byte[8];
                Array.Copy(cdata, data, 8);
            }
            else
            {
                data = Properties.Crypto.Run(key, cardCtx.Buffer, iv);
            }
            HandleBuffer(cardCtx, data);
        }
    }
}
