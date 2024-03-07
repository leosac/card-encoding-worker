using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding.Services
{
    /// <summary>
    /// Crypto encoding service properties.
    /// </summary>
    public class CryptoDataService : EncodingServiceProperties
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public CryptoDataService()
        {
            Crypto = new CryptoProperties();
        }

        /// <summary>
        /// The key to use fo the crypto operation.
        /// </summary>
        public KeyReference? Key { get; set; }

        /// <summary>
        /// The cryptographic properties.
        /// </summary>
        public CryptoProperties Crypto { get; set; }

        /// <summary>
        /// A credential field to use as Initialization Vector.
        /// </summary>
        public string? InitializationVectorField { get; set; }
    }
}
