namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Debit operation.
    /// </summary>
    public class Debit : DESFireActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Debit";

        /// <summary>
        /// The targeted file number.
        /// </summary>
        public byte FileNo { get; set; }

        /// <summary>
        /// The value to credit.
        /// </summary>
        public uint Value { get; set; }

        /// <summary>
        /// The communication encryption mode with the file.
        /// </summary>
        public EncryptionMode EncryptionMode { get; set; }
    }
}
