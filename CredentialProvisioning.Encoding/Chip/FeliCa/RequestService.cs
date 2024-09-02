namespace Leosac.CredentialProvisioning.Encoding.Chip.FeliCa
{
    /// <summary>
    /// Request Service.
    /// </summary>
    public class RequestService : FeliCaActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Request Service";

        /// <summary>
        /// The service code.
        /// </summary>
        public ushort Code { get; set; }
    }
}
