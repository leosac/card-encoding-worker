using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Authenticate EV2 First with a PICC key.
    /// </summary>
    public class AuthenticateEV2First : DESFireActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Authenticate EV2 First";

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
