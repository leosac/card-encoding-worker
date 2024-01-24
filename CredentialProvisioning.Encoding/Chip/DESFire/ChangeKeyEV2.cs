namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Change a PICC key.
    /// </summary>
    public class ChangeKeyEV2 : ChangeKey
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Change Key EV2";

        /// <summary>
        /// The KeySet number.
        /// </summary>
        public byte KeySet { get; set; }
    }
}
