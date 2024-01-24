namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Create a delegated application.
    /// </summary>
    public class CreateDelegatedApplication : DESFireActionProperties
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public CreateDelegatedApplication()
        {
            KeyType = DESFireKeyType.AES128;
            FidSupport = FidSupport.Disabled;
        }

        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Create Delegated Application";

        /// <summary>
        /// The DAM slot number.
        /// </summary>
        public ushort DAMSlotNo { get; set; }

        /// <summary>
        /// The DAM slot version.
        /// </summary>
        public byte DAMSlotVersion { get; set; }

        /// <summary>
        /// A quota limit for the DAM.
        /// </summary>
        public ushort QuotaLimit { get; set; }

        /// <summary>
        /// The new application identifier.
        /// </summary>
        public uint AID { get; set; }

        /// <summary>
        /// The key settings for the new application.
        /// </summary>
        public DESFireKeySettings KeySettings { get; set; }

        /// <summary>
        /// Number of PICC keys inside the application.
        /// </summary>
        public byte MaxNbKeys { get; set; }

        /// <summary>
        /// The key type.
        /// </summary>
        public DESFireKeyType KeyType { get; set; }

        /// <summary>
        /// FID Support.
        /// </summary>
        public FidSupport FidSupport { get; set; }

        /// <summary>
        /// Optional ISO FID.
        /// </summary>
        public ushort? IsoFID { get; set; }

        /// <summary>
        /// Optional ISO DF Name.
        /// </summary>
        public string? IsoDFName { get; set; }

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
