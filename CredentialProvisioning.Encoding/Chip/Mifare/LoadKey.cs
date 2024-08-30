using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding.Chip.Mifare
{
    /// <summary>
    /// Load Key.
    /// </summary>
    public class LoadKey : MifareActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Load Key";

        /// <summary>
        /// The key number.
        /// </summary>
        public byte KeyNo { get; set; }

        /// <summary>
        /// The key type.
        /// </summary>
        public MifareKeyType KeyType { get; set; } = MifareKeyType.KeyA;

        /// <summary>
        /// The key to use for authentication.
        /// </summary>
        public KeyReference? Key { get; set; }
    }
}
