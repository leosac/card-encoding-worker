namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Create a new linear record file.
    /// </summary>
    public class CreateLinearRecordFile : CreateFile
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Create Linear Record File";

        /// <summary>
        /// Size of a record (in bytes).
        /// </summary>
        public uint RecordSize { get; set; }

        /// <summary>
        /// Maximal number of records inside the file.
        /// </summary>
        public uint MaxNumberOfRecords { get; set; }
    }
}
