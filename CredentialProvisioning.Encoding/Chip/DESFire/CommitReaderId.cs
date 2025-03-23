namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Commit the reader identifier.
    /// </summary>
    public class CommitReaderId : DESFireActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Commit Reader ID";

        /// <summary>
        /// The reader identifier.
        /// </summary>
        public string? ReaderId { get ; set; } 
    }
}
