namespace Leosac.CredentialProvisioning.Encoding.Chip.FeliCa
{
    /// <summary>
    /// Write data.
    /// </summary>
    public class Write : FeliCaActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Write";

        /// <summary>
        /// The service code.
        /// </summary>
        public ushort Code { get; set; }

        /// <summary>
        /// The block.
        /// </summary>
        public ushort Block { get; set; }
    }
}
