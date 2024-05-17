namespace Leosac.CredentialProvisioning.Encoding.Services
{
    /// <summary>
    /// Service to dynamically update action property.
    /// </summary>
    public class ChangeActionPropertyService : EncodingServiceProperties
    {
        /// <summary>
        /// The action property name to update.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// The data field name from where to get the updated data.
        /// </summary>
        public string? SourceField { get; set; }

        /// <summary>
        /// The fragment template property from where to get the updated data.
        /// </summary>
        public string? SourceProperty { get; set; }
    }
}
