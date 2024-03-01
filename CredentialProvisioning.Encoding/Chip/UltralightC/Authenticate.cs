using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding.Chip.UltralightC
{
    /// <summary>
    /// Authenticate.
    /// </summary>
    public class Authenticate : UltralightCActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Authenticate";

        /// <summary>
        /// The key to use for authentication.
        /// </summary>
        public KeyReference? Key { get; set; }
    }
}
