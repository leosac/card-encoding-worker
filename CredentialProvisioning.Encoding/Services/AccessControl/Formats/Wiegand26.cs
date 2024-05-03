namespace Leosac.CredentialProvisioning.Encoding.Services.AccessControl.Formats
{
    /// <summary>
    /// Wiegand 26 access control format.
    /// </summary>
    public class Wiegand26 : AccessControlFormat
    {
        /// <summary>
        /// The facility code.
        /// </summary>
        public byte FacilityCode { get; set; }
    }
}
