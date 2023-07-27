namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Write a new record to a file.
    /// </summary>
    public class WriteRecord : DESFireActionProperties
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public WriteRecord()
        {
            EncryptionMode = EncryptionMode.CM_ENCRYPT;
        }

        public override string Name => "Write Record";

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
