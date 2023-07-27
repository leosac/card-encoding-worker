using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Set new PICC configuration.
    /// </summary>
    public class SetConfiguration : DESFireActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Set Configuration";

        /// <summary>
        /// True to enable format/erase operation, false otherwise.
        /// </summary>
        public bool FormatCardEnabled { get; set; }

        /// <summary>
        /// True to enable Random ID/CSN, false otherwise.
        /// </summary>
        public bool RandomIdEnabled { get; set; }

        /// <summary>
        /// Optionally define a new default key.
        /// </summary>
        public KeyReference? DefaultKey { get; set; }
    }
}
