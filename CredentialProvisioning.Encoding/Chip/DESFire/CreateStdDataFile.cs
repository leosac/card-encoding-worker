namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class CreateStdDataFile : CreateFile
    {
        public override string Name => "Create Standard Data File";

        public uint FileSize { get; set; }
    }
}
