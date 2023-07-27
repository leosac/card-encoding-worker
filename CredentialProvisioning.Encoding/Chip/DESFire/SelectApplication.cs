namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Select an existing application.
    /// </summary>
    public class SelectApplication : DESFireActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Select Application";

        /// <summary>
        /// The targeted application identifier.
        /// </summary>
        public uint AID { get; set; }
    }
}
