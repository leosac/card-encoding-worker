using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Create the transaction MAC file.
    /// </summary>
    public class CreateTransactionMACFile : CreateFile
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Create Transaction MAC File";

        /// <summary>
        /// The transaction MAC key.
        /// </summary>
        public KeyReference? Key { get; set; }
    }
}
