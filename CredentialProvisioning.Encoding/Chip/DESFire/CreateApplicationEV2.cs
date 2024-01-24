namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Create a new EV2 application.
    /// </summary>
    public class CreateApplicationEV2 : CreateApplicationEV1
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public CreateApplicationEV2()
        {
            
        }

        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Create Application EV2";

        /// <summary>
        /// Number of keysets on the application.
        /// </summary>
        public byte NumberKeySets { get; set; }

        /// <summary>
        /// Max key size.
        /// </summary>
        public byte MaxKeySize { get; set; }

        /// <summary>
        /// The current keyset version.
        /// </summary>
        public byte ActualKeySetVersion { get; set; }

        /// <summary>
        /// The key number for keyset rolling.
        /// </summary>
        public byte RollKeyNo { get; set; }

        /// <summary>
        /// True to enable application specific capability data, false otherwise.
        /// </summary>
        public bool SpecificCapabilityData { get; set; }

        /// <summary>
        /// True to enable application specific virtual card keys (including proximity check), false otherwise.
        /// </summary>
        public bool SpecificVCKeys { get; set; }
    }
}
