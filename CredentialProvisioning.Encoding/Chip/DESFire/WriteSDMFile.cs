namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Write on SDM file. Always as a URI record.
    /// </summary>
    public class WriteSDMFile : DESFireActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Write on SDM File";

        /// <summary>
        /// The base uri.
        /// </summary>
        public string? BaseUri { get; set; }

        /// <summary>
        /// The VCUID parameter name, if expected.
        /// </summary>
        public string? ParamVcuid { get; set; }

        /// <summary>
        /// The PICC parameter name, if expected.
        /// </summary>
        public string? ParamPicc { get; set; } = "picc";

        /// <summary>
        /// The Read Counter parameter name, if expected.
        /// </summary>
        public string? ParamReadCtr { get; set; }

        /// <summary>
        /// The MAC parameter name, if expected.
        /// </summary>
        public string? ParamMac { get; set; } = "mac";

        /// <summary>
        /// The ISO FID for the NDEF file.
        /// </summary>
        public ushort IsoFIDNDEFFile { get; set; } = 0xe104;
    }
}
