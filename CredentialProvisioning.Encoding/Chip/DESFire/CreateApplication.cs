namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Create a new application.
    /// </summary>
    public class CreateApplication : DESFireActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Create Application";

        /// <summary>
        /// The new application identifier.
        /// </summary>
        public uint AID { get; set; }

        /// <summary>
        /// The key settings for the new application.
        /// </summary>
        public DESFireKeySettings KeySettings { get; set; }

        /// <summary>
        /// Number of PICC keys inside the application.
        /// </summary>
        public byte MaxNbKeys { get; set; }
    }
}
