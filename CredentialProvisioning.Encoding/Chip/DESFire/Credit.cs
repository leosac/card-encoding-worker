namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class Credit : DESFireActionProperties
    {
        public override string Name => "Credit";

        public byte FileNo { get; set; }

        public uint Value { get; set; }

        public EncryptionMode EncryptionMode { get; set; }
    }
}
