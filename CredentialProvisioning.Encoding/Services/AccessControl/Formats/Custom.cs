namespace Leosac.CredentialProvisioning.Encoding.Services.AccessControl.Formats
{
    /// <summary>
    /// Definition of an access control format.
    /// </summary>
    public class Custom : AccessControlFormat
    {
        /// <summary>
        /// The format name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The format fields.
        /// </summary>
        public AccessControlDataField[] Fields { get; set; } = [];
    }
}
