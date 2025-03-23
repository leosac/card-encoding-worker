namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Change a file settings EV3.
    /// </summary>
    public class ChangeFileSettingsEV3 : ChangeFileSettingsEV2
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Change File Settings EV3";

        /// <summary>
        /// Enable SDM/Mirroring.
        /// </summary>
        public bool SdmAndMirroring { get; set; }

        /// <summary>
        /// Optional TMC Limit, if the file is a Transaction MAC file.
        /// </summary>
        public uint TmcLimit { get; set; }

        public bool SdmVcuid { get; set; }

        public bool SdmReadCtr { get; set; }

        public bool SdmReadCtrLimit { get; set; }

        public bool SdmEncFileData { get; set; }

        public bool SdmAsciiEncoding { get; set; }

        public DESFireAccessRights SdmAccessRights { get; set; }

        public uint SdmVcuidOffset { get; set; }

        public uint SdmReadCtrOffset { get; set; }

        public uint SdmPiccDataOffset { get; set; }

        public uint SdmMacInputOffset { get; set; }

        public uint SdmEncOffset { get; set; }

        public uint SdmEncLength { get; set; }

        public uint SdmReadCtrLimitValue { get; set; }

        public uint SdmMacOffset { get; set; }
    }
}
