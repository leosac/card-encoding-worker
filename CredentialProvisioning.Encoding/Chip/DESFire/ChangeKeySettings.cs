namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Change key settings of the current application.
    /// </summary>
    public class ChangeKeySettings : DESFireActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Change Key Settings";

        /// <summary>
        /// The new key settings.
        /// </summary>
        public DESFireKeySettings KeySettings { get; set; }

        /// <summary>
        /// The change key.
        /// </summary>
        public byte ChangeKey { get; set; }
    }
}
