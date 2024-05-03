using System.Reflection;

namespace Leosac.CredentialProvisioning.Encoding.Services.AccessControl
{
    /// <summary>
    /// Base class for an Access Control data field.
    /// </summary>
    public abstract class AccessControlDataField : ICloneable
    {
        /// <summary>
        /// Discriminator for Data Field serialization
        /// </summary>
        public const string Discriminator = "$type";

        /// <summary>
        /// The field name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Clone the data field.
        /// </summary>
        /// <returns>The cloned object.</returns>
        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Get all data field types from the executing assemblyss.
        /// </summary>
        /// <returns>The data field types.</returns>
        public static IEnumerable<Type> GetAllTypes()
        {
            var bt = typeof(AccessControlDataField);
            return Assembly.GetExecutingAssembly().GetTypes().Where(t => bt.IsAssignableFrom(t) && !t.IsAbstract);
        }
    }
}
