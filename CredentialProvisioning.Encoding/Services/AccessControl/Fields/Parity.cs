namespace Leosac.CredentialProvisioning.Encoding.Services.AccessControl.Fields
{
    /// <summary>
    /// Parity data field.
    /// </summary>
    public class Parity : AccessControlDataField
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
