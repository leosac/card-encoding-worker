namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Create a new standard data file EV3.
    /// </summary>
    public class CreateStdDataFileEV3 : CreateStdDataFileEV2
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Create Standard Data File EV3";

        /// <summary>
        /// Enable SDM/Mirroring.
        /// </summary>
        public bool SDMAndMirroring { get; set; }
    }
}
