namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class WriteRecord : DESFireActionProperties
    {
        public WriteRecord()
        {
            EncryptionMode = EncryptionMode.CM_ENCRYPT;
        }

        public override string Name => "Write Record";

        public byte FileNo { get; set; }

        public uint Offset { get; set; }

        public EncryptionMode EncryptionMode { get; set; }
    }
}
