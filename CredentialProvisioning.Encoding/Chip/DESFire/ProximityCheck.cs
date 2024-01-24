using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Perform the full proximity check.
    /// </summary>
    public class ProximityCheck : DESFireActionProperties
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ProximityCheck()
        {
            ChunkSize = 4;
        }

        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Proximity Check";

        /// <summary>
        /// The key to use for proximity check authentication.
        /// </summary>
        public KeyReference? Key { get; set; }

        /// <summary>
        /// The chunk size. This value must be either 1, 2 or 4
        /// </summary>
        public byte ChunkSize { get; set; }
    }
}
