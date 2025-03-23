namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Change a file settings EV2.
    /// </summary>
    public class ChangeFileSettingsEV2 : DESFireActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Change File Settings EV2";

        /// <summary>
        /// The targeted file number to change settings for.
        /// </summary>
        public byte FileNo { get; set; }

        /// <summary>
        /// The new security communication with the file.
        /// </summary>
        public EncryptionMode EncryptionMode { get; set; }

        /// <summary>
        /// The new file access rights.
        /// </summary>
        public DESFireAccessRights[] AccessRights { get; set; } = new DESFireAccessRights[1];
    }
}
