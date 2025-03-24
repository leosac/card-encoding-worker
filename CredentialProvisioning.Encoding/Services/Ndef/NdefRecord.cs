using System.Reflection;

namespace Leosac.CredentialProvisioning.Encoding.Services.Ndef
{
    /// <summary>
    /// Base class for NDEF record definition.
    /// </summary>
    public abstract class NdefRecord : ICloneable
    {
        /// <summary>
        /// Discriminator for NDEF record serialization
        /// </summary>
        public const string Discriminator = "$type";

        /// <summary>
        /// Clone the data field.
        /// </summary>
        /// <returns>The cloned object.</returns>
        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Get all NDEF records types from the executing assembly.
        /// </summary>
        /// <returns>The NDEF record types.</returns>
        public static IEnumerable<Type> GetAllTypes()
        {
            var bt = typeof(NdefRecord);
            return Assembly.GetExecutingAssembly().GetTypes().Where(t => bt.IsAssignableFrom(t) && !t.IsAbstract);
        }
    }
}
