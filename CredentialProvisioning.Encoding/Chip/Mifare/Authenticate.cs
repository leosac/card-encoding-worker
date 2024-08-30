namespace Leosac.CredentialProvisioning.Encoding.Chip.Mifare
{
    /// <summary>
    /// Authenticate.
    /// </summary>
    public class Authenticate : MifareActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Authenticate";

        /// <summary>
        /// The block number.
        /// </summary>
        public byte Block { get; set; }

        /// <summary>
        /// The key number.
        /// </summary>
        public byte KeyNo { get; set; }

        /// <summary>
        /// The key type.
        /// </summary>
        public MifareKeyType KeyType { get; set; } = MifareKeyType.KeyA;
    }
}
