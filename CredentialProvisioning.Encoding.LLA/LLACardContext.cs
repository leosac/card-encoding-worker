namespace Leosac.CredentialProvisioning.Encoding.LLA
{
    public class LLACardContext : CardContext
    {
        public LLACardContext(EncodingDeviceContext deviceContext) : base(deviceContext) { }

        public LLADeviceContext LLADeviceContext
        {
            get => DeviceContext as LLADeviceContext;
        }

        public LibLogicalAccess.Chip? Chip { get; set; }
    }
}
