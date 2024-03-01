using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding.Chip.UltralightC
{
    /// <summary>
    /// Change the PICC key.
    /// </summary>
    public class ChangeKey : UltralightCActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Change Key";

        /// <summary>
        /// The new key.
        /// </summary>
        public KeyReference? Key { get; set; }
    }
}
