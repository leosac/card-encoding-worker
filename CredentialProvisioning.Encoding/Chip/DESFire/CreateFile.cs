namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Base class for file creation.
    /// </summary>
    public abstract class CreateFile : DESFireActionProperties
    {
        /// <summary>
        /// The file number.
        /// </summary>
        public byte FileNo { get; set; }

        /// <summary>
        /// The encryption mode for the file.
        /// </summary>
        public EncryptionMode EncryptionMode { get; set; }

        /// <summary>
        /// The file access rights.
        /// </summary>
        public DESFireAccessRights AccessRights { get; set; }
    }
}
