namespace Leosac.CredentialProvisioning.Encoding.LLA
{
    public class LLADeviceContext : EncodingDeviceContext
    {
        public LibLogicalAccess.Reader.ISO7816ReaderUnit? ReaderUnit { get; set; }

        public LibLogicalAccess.Chip? Chip { get; set; }
    }
}
