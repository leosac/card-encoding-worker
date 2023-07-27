namespace Leosac.CredentialProvisioning.Encoding.Key
{
    /// <summary>
    /// The key reference.
    /// </summary>
    public class KeyReference
    {
        /// <summary>
        /// The key identifier.
        /// </summary>
        public string KeyId { get; set; }

        /// <summary>
        /// Optional key diversification.
        /// </summary>
        public KeyDiversification? Diversification { get; set; }
    }
}
