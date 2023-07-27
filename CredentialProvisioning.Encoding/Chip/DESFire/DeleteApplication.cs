namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Delete an existing application.
    /// </summary>
    public class DeleteApplication : DESFireActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Delete Application";

        /// <summary>
        /// The targeted application identifier.
        /// </summary>
        public uint AID { get; set; }
    }
}
