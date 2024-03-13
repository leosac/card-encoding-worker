namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Initialize a keyset.
    /// </summary>
    public class InitializeKeySet : DESFireActionProperties
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public InitializeKeySet()
        {
            KeyType = DESFireKeyType.AES128;
        }

        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Initialize KeySet";

        /// <summary>
        /// The keyset number.
        /// </summary>
        public byte KeySetNo { get; set; }

        /// <summary>
        /// The key type.
        /// </summary>
        public DESFireKeyType KeyType { get; set; }
    }
}
