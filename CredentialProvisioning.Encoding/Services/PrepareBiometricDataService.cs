namespace Leosac.CredentialProvisioning.Encoding.Services
{
    /// <summary>
    /// Prepare Biometric data encoding service, for further writing.
    /// </summary>
    public class PrepareBiometricDataService : EncodingServiceProperties
    {
        /// <summary>
        /// Biometric product.
        /// </summary>
        public enum BiometricProduct
        {
            /// <summary>
            /// Morpho Access reader.
            /// </summary>
            MorphoAccess,
            /// <summary>
            /// Morpho Access SIGMA reader.
            /// </summary>
            MorphoAccessSIGMA,
            /// <summary>
            /// STid biometric reader.
            /// </summary>
            STid
        }

        public enum TemplateFormat
        {
            Morpho_CFV = 0,
            [Obsolete("TemplateFormat.Sagem_CFV is obsolete. Use TemplateFormat.Morpho_CFV instead.")]
            Sagem_CFV = 0,
            Morpho_PkMat = 1,
            [Obsolete("TemplateFormat.Sagem_PKMAT is obsolete. Use TemplateFormat.Morpho_PkMat instead.")]
            Sagem_PKMAT = 1,
            Morpho_PkCompV2 = 2,
            [Obsolete("TemplateFormat.Sagem_PKCOMPV2 is obsolete. Use TemplateFormat.Morpho_PkCompV2 instead.")]
            Sagem_PKCOMPV2 = 2,
            Morpho_PkLite = 3,
            ANSI_378 = 10,
            ANSI_378_2009 = 11,
            ISO_19794_2 = 20,
            ISO_19794_2_2011 = 23,
            ISO_19794_2_CF_NS = 21,
            ISO_19794_2_CF_CS = 22
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PrepareBiometricDataService()
        {
            BufferBehavior = EncodingServiceBufferBehavior.Overwrite;
            Product = BiometricProduct.MorphoAccess;
            Format = TemplateFormat.Morpho_CFV;
        }

        /// <summary>
        /// The targeted biometric product.
        /// </summary>
        public BiometricProduct Product { get; set; }

        /// <summary>
        /// The template format.
        /// </summary>
        public TemplateFormat Format { get; set; }

        /// <summary>
        /// The finger templates fields.
        /// </summary>
        public string[] Templates { get; set; } = ["BioData_0"];

        /// <summary>
        /// The CardId field (Morpho only).
        /// </summary>
        public string? CardIdField { get; set; } = "Uid";

        /// <summary>
        /// The Username field (Morpho only).
        /// </summary>
        public string? UsernameField { get; set; } = "Username";

        /// <summary>
        /// The duress finger field (Morpho only).
        /// </summary>
        public string? DuressFingerField { get; set; }

        /// <summary>
        /// Fingerprint exemption field (STid only).
        /// </summary>
        public string? FingerExemptionField { get; set; }
    }
}
