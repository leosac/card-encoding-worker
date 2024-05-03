namespace Leosac.CredentialProvisioning.Encoding.Services.AccessControl.Formats
{
    /// <summary>
    /// Wiegand 37 with Facility Code access control format.
    /// </summary>
    public class Wiegand37WithFacility : AccessControlFormat
    {
        /// <summary>
        /// The facility code.
        /// </summary>
        public ushort FacilityCode { get; set; }
    }
}
