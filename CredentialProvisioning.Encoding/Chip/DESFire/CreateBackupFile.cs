namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Create a new backup file.
    /// </summary>
    public class CreateBackupFile : CreateFile
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Create Backup File";

        /// <summary>
        /// The new file size (in bytes).
        /// </summary>
        public uint FileSize { get; set; }
    }
}
