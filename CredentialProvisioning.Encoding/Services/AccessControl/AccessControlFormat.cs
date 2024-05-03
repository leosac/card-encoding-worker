using System.Reflection;

namespace Leosac.CredentialProvisioning.Encoding.Services.AccessControl
{
    /// <summary>
    /// Base class for Access Control format.
    /// </summary>
    public abstract class AccessControlFormat : ICloneable
    {
        /// <summary>
        /// Discriminator for access control format serialization
        /// </summary>
        public const string Discriminator = "$type";

        /// <summary>
        /// Clone the access control format.
        /// </summary>
        /// <returns>The cloned object.</returns>
        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Get all access control format types from the executing assemblyss.
        /// </summary>
        /// <returns>The access control format types.</returns>
        public static IEnumerable<Type> GetAllTypes()
        {
            var bt = typeof(AccessControlFormat);
            return Assembly.GetExecutingAssembly().GetTypes().Where(t => bt.IsAssignableFrom(t) && !t.IsAbstract);
        }
    }
}
