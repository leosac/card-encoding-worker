namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Set new PICC configuration.
    /// </summary>
    public class SetConfigurationEV2 : SetConfiguration
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Set Configuration EV2";

        /// <summary>
        /// True to enforce Proximity Check, false otherwise.
        /// </summary>
        public bool PCMandatoryEnabled { get; set; }

        /// <summary>
        /// True to enforce Virtual Card authentication, false otherwise.
        /// </summary>
        public bool AuthVCMandatoryEnabled { get; set; }
    }
}
