namespace Leosac.CredentialProvisioning.Encoding.Chip.ISO15693
{
    /// <summary>
    /// Write DSFID.
    /// </summary>
    public class WriteDSFID : ISO15693ActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Write DSFID";

        /// <summary>
        /// The DSFID value.
        /// </summary>
        public uint DSFID { get; set; }
    }
}
