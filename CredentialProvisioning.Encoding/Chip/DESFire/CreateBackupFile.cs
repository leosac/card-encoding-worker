namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class CreateBackupFile : CreateFile
    {
        public override string Name => "Create Backup File";

        public uint FileSize { get; set; }
    }
}
