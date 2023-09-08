namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Create a new EV1 application.
    /// </summary>
    public class CreateApplicationEV1 : CreateApplication
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public CreateApplicationEV1()
        {
            KeyType = DESFireKeyType.AES128;
            FidSupport = FidSupport.Disabled;
        }

        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Create Application EV1";

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
    }
}
