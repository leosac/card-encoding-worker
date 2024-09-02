namespace Leosac.CredentialProvisioning.Encoding.Chip.FeliCa
{
    /// <summary>
    /// Read data.
    /// </summary>
    public class Read : FeliCaActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Read";

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
