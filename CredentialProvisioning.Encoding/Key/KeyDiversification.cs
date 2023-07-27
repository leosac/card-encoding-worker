namespace Leosac.CredentialProvisioning.Encoding.Key
{
    /// <summary>
    /// Key Diversification details.
    /// </summary>
    public class KeyDiversification
    {
        /// <summary>
        /// The diversification algorithm.
        /// </summary>
        public string Algorithm { get; set; }

        /// <summary>
        /// Div Input fragments to use.
        /// </summary>
        public DivInputFragment[] Input { get; set; }

        /// <summary>
        /// Use revert AID.
        /// </summary>
        /// <remarks>
        /// For AN10922 diversification only.
        /// </remarks>
        public bool? RevertAID { get; set; }

        /// <summary>
        /// For K2 use.
        /// </summary>
        /// <remarks>
        /// For AN10922 diversification only.
        /// </remarks>
        public bool? ForceK2Use { get; set; }

        /// <summary>
        /// The System Identifier.
        /// </summary>
        /// <remarks>
        /// For AN10922 diversification only.
        /// </remarks>
        public string? SystemIdentifier { get; set; }
    }
}
