namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class CreateLinearRecordFile : CreateFile
    {
        public override string Name => "Create Linear Record File";

        public uint RecordSize { get; set; }

        public uint MaxNumberOfRecords { get; set; }
    }
}
