namespace Leosac.CredentialProvisioning.Encoding.Services.AccessControl
{
    /// <summary>
    /// Parity data field.
    /// </summary>
    public class ParityDataField : AccessControlDataField
    {
        /// <summary>
        /// The parity type.
        /// </summary>
        public ParityType ParityType { get; set; }

        /// <summary>
        /// Bits to use for parity calculation.
        /// </summary>
        public int[] Bits = [];
    }
}
