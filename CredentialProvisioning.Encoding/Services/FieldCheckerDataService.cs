namespace Leosac.CredentialProvisioning.Encoding.Services
{
    /// <summary>
    /// Field Checker encoding service properties.
    /// </summary>
    public class FieldCheckerDataService : EncodingServiceProperties
    {
        /// <summary>
        /// The field to check.
        /// </summary>
        public string Field { get; set; } = string.Empty;

        /// <summary>
        /// The expected value to check against.
        /// </summary>
        public string? ExpectedValue { get; set; }

        /// <summary>
        /// Whether the field is required or not.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Whether to reverse the check (i.e. check that the field does NOT match the value).
        /// </summary>
        public bool ReverseCheck { get; set; }
    }
}
