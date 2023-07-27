using System.Reflection;

namespace Leosac.CredentialProvisioning.Encoding
{
    /// <summary>
    /// Base class for encoding service properties.
    /// </summary>
    public abstract class EncodingServiceProperties
    {
        public const string Discriminator = "typeName";

        /// <summary>
        /// The behavior on the temporary buffer shared between services and actions.
        /// </summary>
        public EncodingServiceBufferBehavior BufferBehavior { get; set; }

        /// <summary>
        /// An optional prefix to apply to field names of the service.
        /// </summary>
        public string? FieldNamePrefix { get; set; }

        /// <summary>
        /// Get all encoding services properties types from the executing assemblyss.
        /// </summary>
        /// <returns>The encoding services properties types.</returns>
        public static IEnumerable<Type> GetAllTypes()
        {
            var bt = typeof(EncodingServiceProperties);
            return Assembly.GetExecutingAssembly().GetTypes().Where(t => bt.IsAssignableFrom(t) && !t.IsAbstract);
        }
    }
}
