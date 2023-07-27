namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Write data to a file.
    /// </summary>
    public class WriteData : DESFireActionProperties
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public WriteData()
        {
            EncryptionMode = EncryptionMode.CM_ENCRYPT;
        }

        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Write Data";

        /// <summary>
        /// The targeted file number.
        /// </summary>
        public byte FileNo { get; set; }

        /// <summary>
        /// The offset.
        /// </summary>
        public uint Offset { get; set; }

        /// <summary>
        /// The communication encryption mode with the file.
        /// </summary>
        public EncryptionMode EncryptionMode { get; set; }
    }
}
