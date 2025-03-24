namespace Leosac.CredentialProvisioning.Encoding.Services.Ndef
{
    /// <summary>
    /// Text NDEF record.
    /// </summary>
    public class TextRecord : NdefRecord
    {
        /// <summary>
        /// The text language.
        /// </summary>
        public string? Language { get; set; }

        /// <summary>
        /// The text value.
        /// </summary>
        public string? Text { get; set; }
    }
}
