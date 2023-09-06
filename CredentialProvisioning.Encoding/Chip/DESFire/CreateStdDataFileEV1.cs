
namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Create a new standard data file EV1.
    /// </summary>
    public class CreateStdDataFileEV1 : CreateStdDataFile
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Create Standard Data File EV1";

        /// <summary>
        /// Optional ISO FID.
        /// </summary>
        public ushort? IsoFID { get; set; }
    }
}
