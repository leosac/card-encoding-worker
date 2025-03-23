namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Restore the value from one value file to another.
    /// </summary>
    public class RestoreTransfer : DESFireActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Restore Transfer";

        /// <summary>
        /// The targeted file number.
        /// </summary>
        public byte TargetFileNo { get; set; }

        /// <summary>
        /// The source file number.
        /// </summary>
        public byte SourceFileNo { get; set; }
    }
}
