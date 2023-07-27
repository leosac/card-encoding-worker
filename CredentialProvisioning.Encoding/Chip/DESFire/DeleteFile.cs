namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Delete an existing file.
    /// </summary>
    public class DeleteFile : DESFireActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Delete File";

        /// <summary>
        /// The targeted file number.
        /// </summary>
        public byte FileNo { get; set; }
    }
}
