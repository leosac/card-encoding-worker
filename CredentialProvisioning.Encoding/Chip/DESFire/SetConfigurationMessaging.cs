namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Set global secure messaging configuration.
    /// </summary>
    public class SetConfigurationMessaging : DESFireActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Set Configuration - Secure Messaging";

        /// <summary>
        /// True to enable D40 secure messaging, false otherwise.
        /// </summary>
        public bool D40SecureMessagingEnabled { get; set; }

        /// <summary>
        /// True to enable EV1 secure messaging, false otherwise.
        /// </summary>
        public bool EV1SecureMessagingEnabled { get; set; }

        /// <summary>
        /// True to enable EV2 chanined writing, false otherwise.
        /// </summary>
        public bool EV2ChainedWritingEnabled { get; set; }
    }
}
