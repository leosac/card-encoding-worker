namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class ChangeFileSettings : DESFireActionProperties
    {
        public override string Name => "Change File Settings";

        public byte FileNo { get; set; }

        public EncryptionMode EncryptionMode { get; set; }

        public DESFireAccessRights AccessRights { get; set; }

        public bool Plain { get; set; }
    }
}
