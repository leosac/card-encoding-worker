namespace Leosac.CredentialProvisioning.Encoding.Services.AccessControl.Formats
{
    /// <summary>
    /// Wiegand 34 with Facility Code access control format.
    /// </summary>
    public class Wiegand34WithFacility : AccessControlFormat
    {
        /// <summary>
        /// The facility code.
        /// </summary>
        public ushort FacilityCode { get; set; }
    }
}
