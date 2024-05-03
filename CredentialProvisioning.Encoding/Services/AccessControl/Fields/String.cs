namespace Leosac.CredentialProvisioning.Encoding.Services.AccessControl.Fields
{
    public class String : ValueDataField
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
