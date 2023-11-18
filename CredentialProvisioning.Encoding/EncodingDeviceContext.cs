using Leosac.CredentialProvisioning.Core.Contexts;
using Leosac.CredentialProvisioning.Core.Models;

namespace Leosac.CredentialProvisioning.Encoding
{
    /// <summary>
    /// The base class for encoding device context.
    /// </summary>
    public abstract class EncodingDeviceContext : DeviceContext
    {
        /// <summary>
        /// Initialize the device context.
        /// </summary>
        /// <param name="cardType">Force the card type</param>
        /// <returns>The executing task which returns true on initialization success, false otherwise.</returns>
        public abstract Task<bool> Initialize(string? cardType = null);

        /// <summary>
        /// Uninitialize the device context.
        /// </summary>
        /// <returns>The executing task.</returns>
        public abstract Task UnInitialize();

        /// <summary>
        /// Prepare a new card for production, returns a new card context.
        /// </summary>
        /// <param name="credential">The targeted credential details.</param>
        /// <returns>The executing task which returns the newly created card context.</returns>
        public abstract Task<CardContext> PrepareCard(WorkerCredentialBase? credential = null);

        /// <summary>
        /// Complete a card production.
        /// </summary>
        /// <param name="context">The card context.</param>
        /// <returns>The executing task.</returns>
        public abstract Task CompleteCard(CardContext context);
    }
}
