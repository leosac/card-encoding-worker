namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class CreateCyclicRecordFile : CreateFile
    {
        public override string Name => "Create Cyclic Record File";

        public uint RecordSize { get; set; }

        public uint MaxNumberOfRecords { get; set; }
    }
}
