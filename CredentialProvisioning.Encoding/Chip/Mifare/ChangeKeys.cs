using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding.Chip.Mifare
{
    /// <summary>
    /// Change Keys.
    /// </summary>
    public class ChangeKeys : MifareActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Change Keys";

        /// <summary>
        /// The sector number.
        /// </summary>
        public byte Sector { get; set; }

        /// <summary>
        /// The new key A.
        /// </summary>
        public KeyReference? KeyA { get; set; }

        /// <summary>
        /// The new key B.
        /// </summary>
        public KeyReference? KeyB { get; set; }

        /// <summary>
        /// The new sector access bits configuration.
        /// </summary>
        public SectorAccessBits SectorAccessBits { get; set; } = SectorAccessBits.Transport;
    }
}
