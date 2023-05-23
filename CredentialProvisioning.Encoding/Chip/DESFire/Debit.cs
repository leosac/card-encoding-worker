namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class Debit : DESFireActionProperties
    {
        public override string Name => "Debit";

        public byte FileNo { get; set; }

        public uint Value { get; set; }

        public EncryptionMode EncryptionMode { get; set; }
    }
}
