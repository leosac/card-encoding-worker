namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class WriteData : DESFireActionProperties
    {
        public WriteData()
        {
            EncryptionMode = EncryptionMode.CM_ENCRYPT;
        }

        public override string Name => "Write Data";

        public byte FileNo { get; set; }

        public uint Offset { get; set; }

        public EncryptionMode EncryptionMode { get; set; }
    }
}
