namespace Leosac.CredentialProvisioning.Encoding.Chip.Mifare
{
    /// <summary>
    /// Read binary data.
    /// </summary>
    public class ReadBinary : MifareActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Read Binary";

        /// <summary>
        /// The block number.
        /// </summary>
        public byte Block { get; set; }

        /// <summary>
        /// Number of bytes to read.
        /// </summary>
        public byte Length { get; set; }
    }
}
