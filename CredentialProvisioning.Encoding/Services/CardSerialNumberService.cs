namespace Leosac.CredentialProvisioning.Encoding.Services
{
    /// <summary>
    /// Card Serial Number encoding service properties.
    /// </summary>
    public class CardSerialNumberService : EncodingServiceProperties
    {
        /// <summary>
        /// True to check for CSN value match, false otherwise.
        /// </summary>
        /// <remarks>If true, the associated credential CSN field must contains the expected CSN value.</remarks>
        public bool CheckCSN { get; set; }
    }
}
