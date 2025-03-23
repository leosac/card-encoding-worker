namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Create a new standard data file EV2.
    /// </summary>
    public class CreateStdDataFileEV2 : CreateStdDataFileEV1
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Create Standard Data File EV2";

        /// <summary>
        /// Multiple access rights.
        /// </summary>
        public bool MultiAccessRights { get; set; }
    }
}
