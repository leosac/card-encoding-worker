using Leosac.CredentialProvisioning.Core.Models;

namespace Leosac.CredentialProvisioning.Encoding.Key
{
    public class KeyProvider
    {
        public KeyProvider()
        {
            Items = new List<CredentialKey>();
        }

        public IList<CredentialKey> Items { get; protected set; }

        public void Add(CredentialKey key)
        {
            lock (Items)
            {
                Remove(key.Id);
                Items.Add(key);
            }
        }

        public CredentialKey? Get(Guid keyId)
        {
            lock (Items)
            {
                return Items.FirstOrDefault(i => i.Id == keyId);
            }
        }

        public CredentialKey? Get(KeyReference keyRef)
        {
            return Get(Guid.Parse(keyRef.KeyId));
        }

        public void Remove(Guid keyId)
        {
            lock (Items)
            {
                var key = Get(keyId);
                if (key != null)
                {
                    Items.Remove(key);
                }
            }
        }
    }
}
