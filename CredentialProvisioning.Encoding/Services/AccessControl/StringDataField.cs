namespace Leosac.CredentialProvisioning.Encoding.Services.AccessControl
{
    public class StringDataField : ValueDataField
    {
        /// <summary>
        /// The string value.
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// The string charset.
        /// </summary>
        public string? Charset { get; set; } = "ascii";

        /// <summary>
        /// The padding char.
        /// </summary>
        public byte PaddingChar { get; set; }
    }
}
