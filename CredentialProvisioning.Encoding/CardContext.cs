using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Worker;

namespace Leosac.CredentialProvisioning.Encoding
{
    /// <summary>
    /// The base card context.
    /// </summary>
    /// <remarks>
    /// Default constructor.
    /// </remarks>
    /// <param name="deviceContext">The associated device context.</param>
    /// <param name="credential">The associated credential details.</param>
    public abstract class CardContext(EncodingDeviceContext deviceContext, WorkerCredentialBase? credential = null) : WorkerCredentialContext(deviceContext, credential)
    {
        /// <summary>
        /// The shared temporary buffer which can be used for encoding actions / services raw data sharing.
        /// </summary>
        public byte[]? Buffer { get; set; }
    }
}
