﻿using System.Reflection;

namespace Leosac.CredentialProvisioning.Encoding
{
    /// <summary>
    /// Base class for encoding service properties.
    /// </summary>
    public abstract class EncodingServiceProperties : ICloneable
    {
        /// <summary>
        /// Discriminator for Encoding Service Properties serialization
        /// </summary>
        public const string Discriminator = "$type";

        /// <summary>
        /// The behavior on the temporary buffer shared between services and actions.
        /// </summary>
        public EncodingServiceBufferBehavior BufferBehavior { get; set; }

        /// <summary>
        /// Copy the buffer, after service execution, to a credential field.
        /// </summary>
        public string? CopyBufferToField { get; set; }

        /// <summary>
        /// An optional prefix to apply to field names of the service.
        /// </summary>
        public string? FieldNamePrefix { get; set; }

        /// <summary>
        /// Clone the encoding service properties.
        /// </summary>
        /// <returns>The cloned object.</returns>
        public virtual object Clone()
        {
            return MemberwiseClone();
        }

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
