namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Roll to keyset.
    /// </summary>
    public class RollKeySet : DESFireActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Roll KeySet";

        /// <summary>
        /// The keyset number.
        /// </summary>
        public byte KeySetNo { get; set; }
    }
}
