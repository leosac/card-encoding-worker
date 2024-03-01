namespace Leosac.CredentialProvisioning.Encoding.Chip.UltralightC
{
    /// <summary>
    /// Write data to a page.
    /// </summary>
    public class WritePage : UltralightCActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Write Page";

        /// <summary>
        /// The page number.
        /// </summary>
        public int Page { get; set; }
    }
}
