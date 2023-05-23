namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public abstract class CreateFile : DESFireActionProperties
    {
        public byte FileNo { get; set; }

        public EncryptionMode EncryptionMode { get; set; }

        public DESFireAccessRights AccessRights { get; set; }
    }
}
