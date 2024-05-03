namespace Leosac.CredentialProvisioning.Encoding.Services.AccessControl.Formats
{
    /// <summary>
    /// Wiegand 35 access control format.
    /// </summary>
    public class Wiegand35 : AccessControlFormat
    {
        /// <summary>
        /// The company code.
        /// </summary>
        public ushort CompanyCode { get; set; }
    }
}
