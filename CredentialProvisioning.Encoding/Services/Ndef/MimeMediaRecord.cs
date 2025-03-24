namespace Leosac.CredentialProvisioning.Encoding.Services.Ndef
{
    /// <summary>
    /// Mime Media NDEF record.
    /// </summary>
    public class MimeMediaRecord : NdefRecord
    {
        /// <summary>
        /// The mime type.
        /// </summary>
        public string? MimeType { get; set; }

        /// <summary>
        /// The binary payload.
        /// </summary>
        public string? Payload { get; set; }
    }
}
