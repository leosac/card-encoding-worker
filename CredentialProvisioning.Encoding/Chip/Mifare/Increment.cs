namespace Leosac.CredentialProvisioning.Encoding.Chip.Mifare
{
    /// <summary>
    /// Increment.
    /// </summary>
    public class Increment : MifareActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Increment";

        /// <summary>
        /// The block number.
        /// </summary>
        public byte Block { get; set; }

        /// <summary>
        /// The value to increment.
        /// </summary>
        public uint Value { get; set; }
    }
}
