namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class ReadData : DESFireActionProperties
    {
        public ReadData()
        {
            EncryptionMode = EncryptionMode.CM_ENCRYPT;
        }

        public override string Name => "Read Data";

        public byte FileNo { get; set; }

        public uint Offset { get; set; }

        public uint ByteLength { get; set; }

        public EncryptionMode EncryptionMode { get; set; }
    }
}
