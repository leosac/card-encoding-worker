namespace Leosac.CredentialProvisioning.Encoding.Chip.ISO15693
{
    /// <summary>
    /// Write data to a block.
    /// </summary>
    public class WriteBlock : ISO15693ActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Write Block";

        /// <summary>
        /// The block.
        /// </summary>
        public uint Block { get; set; }
    }
}
