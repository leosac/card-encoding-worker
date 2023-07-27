namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Create a new standard data file.
    /// </summary>
    public class CreateStdDataFile : CreateFile
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Create Standard Data File";

        /// <summary>
        /// The new file size (in bytes).
        /// </summary>
        public uint FileSize { get; set; }
    }
}
