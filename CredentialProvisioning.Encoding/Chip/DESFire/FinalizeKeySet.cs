namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Finalize a keyset.
    /// </summary>
    public class FinalizeKeySet : DESFireActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Finalize KeySet";

        /// <summary>
        /// The keyset number.
        /// </summary>
        public byte KeySetNo { get; set; }

        /// <summary>
        /// The keyset version.
        /// </summary>
        public byte KeySetVersion { get; set; }
    }
}
