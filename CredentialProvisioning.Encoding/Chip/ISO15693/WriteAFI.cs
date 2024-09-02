namespace Leosac.CredentialProvisioning.Encoding.Chip.ISO15693
{
    /// <summary>
    /// Write AFI.
    /// </summary>
    public class WriteAFI : ISO15693ActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Write AFI";

        /// <summary>
        /// The AFI value.
        /// </summary>
        public uint AFI { get; set; }
    }
}
