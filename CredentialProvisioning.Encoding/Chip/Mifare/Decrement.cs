namespace Leosac.CredentialProvisioning.Encoding.Chip.Mifare
{
    /// <summary>
    /// Decrement.
    /// </summary>
    public class Decrement : MifareActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Decrement";

        /// <summary>
        /// The block number.
        /// </summary>
        public byte Block { get; set; }

        /// <summary>
        /// The value to decrement.
        /// </summary>
        public uint Value { get; set; }
    }
}
