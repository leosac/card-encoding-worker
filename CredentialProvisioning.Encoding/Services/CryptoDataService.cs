using Leosac.CredentialProvisioning.Encoding.Key;
using System.Security.Cryptography;

namespace Leosac.CredentialProvisioning.Encoding.Services
{
    /// <summary>
    /// Crypto encoding service properties.
    /// </summary>
    public class CryptoDataService : EncodingServiceProperties
    {
        /// <summary>
        /// The key to use fo the crypto operation.
        /// </summary>
        public KeyReference? Key { get; set; }

        /// <summary>
        /// The cryptographic operation to perform.
        /// </summary>
        public CryptoOperation Operation { get; set; }

        /// <summary>
        /// The cryptographic hash algorithm to use (if required).
        /// </summary>
        public string? HashAlgorithm { get; set; }

        /// <summary>
        /// The cipher mode (if required).
        /// </summary>
        public CipherMode? CipherMode { get; set; }

        /// <summary>
        /// The padding mode (if required).
        /// </summary>
        public PaddingMode? PaddingMode { get; set; }
    }
}
