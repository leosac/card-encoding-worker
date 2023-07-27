namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Credit operation.
    /// </summary>
    public class Credit : DESFireActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Credit";

        /// <summary>
        /// The targeted file number.
        /// </summary>
        public byte FileNo { get; set; }

        /// <summary>
        /// The value to debit.
        /// </summary>
        public uint Value { get; set; }

        /// <summary>
        /// The communication encryption mode with the file.
        /// </summary>
        public EncryptionMode EncryptionMode { get; set; }
    }
}
