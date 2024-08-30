namespace Leosac.CredentialProvisioning.Encoding.Chip.Mifare
{
    /// <summary>
    /// Write binary data.
    /// </summary>
    public class UpdateBinary : MifareActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Write Binary";

        /// <summary>
        /// The block number.
        /// </summary>
        public byte Block { get; set; }
    }
}
