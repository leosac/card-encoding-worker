namespace Leosac.CredentialProvisioning.Encoding.Services.Ndef
{
    /// <summary>
    /// NDEF URI Type
    /// </summary>
    public enum UriType : byte
    {
        /// <summary>
        /// No prefix
        /// </summary>
        NoPrefix = 0,
        /// <summary>
        /// http://www prefix
        /// </summary>
        HttpWww = 1,
        /// <summary>
        /// https://www prefix
        /// </summary>
        HttpsWww = 2,
        /// <summary>
        /// http:// prefix
        /// </summary>
        Http = 3,
        /// <summary>
        /// https:// prefix
        /// </summary>
        Https = 4,
        /// <summary>
        /// tel:// prefix
        /// </summary>
        Tel = 5,
        /// <summary>
        /// mailto:// prefix
        /// </summary>
        MailTo = 6,
        /// <summary>
        /// file:// prefix
        /// </summary>
        UriFile = 29
    }

    /// <summary>
    /// URI NDEF record.
    /// </summary>
    public class UriRecord : NdefRecord
    {
        /// <summary>
        /// The uri.
        /// </summary>
        public string? Uri { get; set; }

        /// <summary>
        /// The URI prefix type.
        /// </summary>
        public UriType Prefixe { get; set; }
    }
}
