using Leosac.CredentialProvisioning.Core.Models;

namespace Leosac.CredentialProvisioning.Encoding.Key
{
    /// <summary>
    /// A basic key provider.
    /// </summary>
    public class KeyProvider
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public KeyProvider()
        {
            Items = new List<CredentialKey>();
        }

        /// <summary>
        /// List of keys currently loaded.
        /// </summary>
        public IList<CredentialKey> Items { get; protected set; }

        /// <summary>
        /// Add a new key to the list.
        /// </summary>
        /// <param name="key">The key to add.</param>
        public void Add(CredentialKey key)
        {
            lock (Items)
            {
                Remove(key.Id);
                Items.Add(key);
            }
        }

        /// <summary>
        /// Get a key from the list.
        /// </summary>
        /// <param name="keyId">The key identifier to look for.</param>
        /// <param name="volatileKeys">Optional volatile keys.</param>
        /// <returns>The matching key, null otherwise.</returns>
        public CredentialKey? Get(Guid keyId, IEnumerable<CredentialKey>? volatileKeys = null)
        {
            lock (Items)
            {
                var key = Items.FirstOrDefault(i => i.Id == keyId);
                if (key == null && volatileKeys != null)
                {
                    key = volatileKeys.FirstOrDefault(k => k.Id == keyId);
                }
                return key;
            }
        }

        /// <summary>
        /// Get a key from th list.
        /// </summary>
        /// <param name="keyRef">The key reference.</param>
        /// <param name="volatileKeys">Optional volatile keys.</param>
        /// <returns>The matching key, null otherwise.</returns>
        public CredentialKey? Get(KeyReference keyRef, IEnumerable<CredentialKey>? volatileKeys = null)
        {
            if (keyRef == null)
                return null;

            return Get(Guid.Parse(keyRef.KeyId));
        }

        /// <summary>
        /// Remove a key from the list.
        /// </summary>
        /// <param name="keyId">The key identifier.</param>
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
