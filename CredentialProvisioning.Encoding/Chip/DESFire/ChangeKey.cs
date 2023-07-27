using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Change a PICC key.
    /// </summary>
    public class ChangeKey : DESFireActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Change Key";

        /// <summary>
        /// The PICC key number.
        /// </summary>
        public byte KeyNo { get; set; }

        /// <summary>
        /// The new key.
        /// </summary>
        public KeyReference? Key { get; set; }
    }
}
