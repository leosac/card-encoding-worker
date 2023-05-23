namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class DeleteFile : DESFireActionProperties
    {
        public override string Name => "Delete File";

        public byte FileNo { get; set; }
    }
}
