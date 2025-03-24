namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Create a new application pre-setup for NDEF record.
    /// </summary>
    public class CreateNFCApplication : DESFireActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Create NFC Application";

        /// <summary>
        /// The new application identifier.
        /// </summary>
        public uint AID { get; set; }

        /// <summary>
        /// The ISO FID for the application.
        /// </summary>
        public ushort IsoFIDApplication { get; set; } = 0xe105;

        /// <summary>
        /// The ISO FID for the Capacility Container file.
        /// </summary>
        public ushort IsoFIDCapabilityContainer { get; set; } = 0xe103;

        /// <summary>
        /// The ISO FID for the NDEF file.
        /// </summary>
        public ushort IsoFIDNDEFFile { get; set; } = 0xe104;

        /// <summary>
        /// The NDEF file size.
        /// </summary>
        public ushort NDEFFileSize { get; set; } = 0xff;
    }
}
