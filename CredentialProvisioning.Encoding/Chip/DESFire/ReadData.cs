namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Read data from a file.
    /// </summary>
    public class ReadData : DESFireActionProperties
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ReadData()
        {
            EncryptionMode = EncryptionMode.Encrypt;
        }

        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Read Data";

        /// <summary>
        /// The targeted file number.
        /// </summary>
        public byte FileNo { get; set; }

        /// <summary>
        /// The offset.
        /// </summary>
        public uint Offset { get; set; }

        /// <summary>
        /// Number of bytes to read.
        /// </summary>
        public uint ByteLength { get; set; }

        /// <summary>
        /// The communication encryption mode with the file.
        /// </summary>
        public EncryptionMode EncryptionMode { get; set; }
    }
}
