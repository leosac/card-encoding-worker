namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Write on NDEF file.
    /// </summary>
    public class WriteNDEFFile : DESFireActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Write NDEF File.";

        /// <summary>
        /// The ISO FID for the NDEF file.
        /// </summary>
        public ushort IsoFIDNDEFFile { get; set; } = 0xe104;
    }
}
