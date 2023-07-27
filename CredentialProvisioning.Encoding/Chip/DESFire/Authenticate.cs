using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Authenticate with a PICC key.
    /// </summary>
    public class Authenticate : DESFireActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Authenticate";

        /// <summary>
        /// The PICC key number.
        /// </summary>
        public byte KeyNo { get; set; }

        /// <summary>
        /// The key to use for authentication.
        /// </summary>
        public KeyReference? Key { get; set; }
    }
}
