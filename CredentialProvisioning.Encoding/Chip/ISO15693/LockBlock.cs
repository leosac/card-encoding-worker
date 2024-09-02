namespace Leosac.CredentialProvisioning.Encoding.Chip.ISO15693
{
    /// <summary>
    /// Lock a block.
    /// </summary>
    public class LockBlock : ISO15693ActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Lock Block";

        /// <summary>
        /// The block.
        /// </summary>
        public uint Block { get; set; }
    }
}
