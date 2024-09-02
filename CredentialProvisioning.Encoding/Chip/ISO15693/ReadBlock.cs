namespace Leosac.CredentialProvisioning.Encoding.Chip.ISO15693
{
    /// <summary>
    /// Read data on a block.
    /// </summary>
    public class ReadBlock : ISO15693ActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Read Block";

        /// <summary>
        /// The block.
        /// </summary>
        public uint Block { get; set; }
    }
}
