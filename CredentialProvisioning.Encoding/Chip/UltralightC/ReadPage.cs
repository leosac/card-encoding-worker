namespace Leosac.CredentialProvisioning.Encoding.Chip.UltralightC
{
    /// <summary>
    /// Read data from a page.
    /// </summary>
    public class ReadPage : UltralightCActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Read Page";

        /// <summary>
        /// The page number.
        /// </summary>
        public int Page { get; set; }
    }
}
