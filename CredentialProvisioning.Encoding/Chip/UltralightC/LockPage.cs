namespace Leosac.CredentialProvisioning.Encoding.Chip.UltralightC
{
    /// <summary>
    /// Lock a page.
    /// </summary>
    public class LockPage : UltralightCActionProperties
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Lock Page";

        /// <summary>
        /// The page number.
        /// </summary>
        public int Page { get; set; }
    }
}
