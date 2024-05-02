using System.Text.Json.Serialization;
using Leosac.CredentialProvisioning.Encoding.Services.AccessControl;

namespace Leosac.CredentialProvisioning.Encoding.Services
{
    /// <summary>
    /// The base access control data encoding service properties.
    /// </summary>
    public abstract class AccessControlDataService : EncodingServiceProperties
    {
        /// <summary>
        /// The XML definition of the access control format to use.
        /// </summary>
        /// <remarks>This field takes priority over FormatDefinition field.</remarks>
        public string? Format { get; set; }

        /// <summary>
        /// The format definition.
        /// </summary>
        public AccessControlFormat? FormatDefinition { get; set; }
    }
}
