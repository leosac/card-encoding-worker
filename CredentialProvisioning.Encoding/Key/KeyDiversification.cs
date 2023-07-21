namespace Leosac.CredentialProvisioning.Encoding.Key
{
    public class KeyDiversification
    {
        public string Algorithm { get; set; }

        public DivInputFragment[] Input { get; set; }

        /// <remarks>
        /// For AN10922 diversification only.
        /// </remarks>
        public bool? RevertAID { get; set; }

        /// <remarks>
        /// For AN10922 diversification only.
        /// </remarks>
        public bool? ForceK2Use { get; set; }

        /// <remarks>
        /// For AN10922 diversification only.
        /// </remarks>
        public string? SystemIdentifier { get; set; }
    }
}
