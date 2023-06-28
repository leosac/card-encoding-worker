using LibLogicalAccess.Card;
using LibLogicalAccess;
using Leosac.CredentialProvisioning.Core.Models;

namespace Leosac.CredentialProvisioning.Encoding.LLA
{
    public static class CredentialKeyExt
    {
        public static Key? CreateKey(this CredentialKey k)
        {
            Key? key = null;
            if (k.KeyType == "aes128")
            {
                var dkey = new DESFireKey();
                dkey.setKeyType(DESFireKeyType.DF_KEY_AES); 
            }
            else if (k.KeyType == "2k3des")
            {
                var dkey = new DESFireKey();
                dkey.setKeyType(DESFireKeyType.DF_KEY_DES);
            }
            else if (k.KeyType == "3k3des")
            {
                var dkey = new DESFireKey();
                dkey.setKeyType(DESFireKeyType.DF_KEY_3K3DES);
            }

            if (key != null)
            {
                if (k.KeyStoreType == "hsm")
                {
                    var ks = new PKCSKeyStorage();
                    ks.set_key_id(new ByteVector(System.Text.Encoding.UTF8.GetBytes(k.KeyStoreReference)));
                    // TODO: set here PKCS#11 library path and password from application configuration file
                    key.setKeyStorage(ks);
                }
                else if (k.KeyStoreType == "sam")
                {
                    var ks = new SAMKeyStorage();
                    byte slot = 0;
                    byte.TryParse(k.KeyStoreReference, out slot);
                    ks.setKeySlot(slot);
                }
                else
                {
                    key.fromString(k.Value);
                }
            }

            return key;
        }
    }
}
