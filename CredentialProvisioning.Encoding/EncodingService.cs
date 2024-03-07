using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding
{
    /// <summary>
    /// The base class for encoding service implementation.
    /// </summary>
    public abstract class EncodingService
    {
        /// <summary>
        /// Run the encoding service.
        /// </summary>
        /// <param name="cardCtx">The card context.</param>
        /// <param name="keystore">The key provider.</param>
        /// <param name="currentAction">The current encoding action from where the service run.</param>
        public abstract void Run(CardContext cardCtx, KeyProvider? keystore, EncodingAction currentAction);
    }

    /// <summary>
    /// The generic base class for encoding service implementation.
    /// </summary>
    /// <typeparam name="T">The encoding service properties.</typeparam>
    public abstract class EncodingService<T> : EncodingService where T : EncodingServiceProperties, new()
    {
        /// <summary>
        /// Base constructor.
        /// </summary>
        /// <param name="properties">The associated encoding service properties.</param>
        protected EncodingService(T properties)
        {
            Properties = properties;
        }

        /// <summary>
        /// The associated encoding service properties.
        /// </summary>
        public T Properties { get; set; } = new T();

        /// <summary>
        /// Handle buffer behavior.
        /// </summary>
        /// <param name="cardCtx">The card context.</param>
        /// <param name="data">The new data to handle on the shared buffer.</param>
        protected void HandleBuffer(CardContext cardCtx, byte[]? data)
        {
            var behavior = Properties.BufferBehavior;
            if (behavior == EncodingServiceBufferBehavior.Prepend && cardCtx.Buffer != null)
            {
                if (data == null)
                    behavior = EncodingServiceBufferBehavior.DoNothing;
                else
                    data = data.Concat(cardCtx.Buffer).ToArray();
            }
            if (behavior == EncodingServiceBufferBehavior.Append && cardCtx.Buffer != null)
            {
                if (data == null)
                    behavior = EncodingServiceBufferBehavior.DoNothing;
                else
                    data = cardCtx.Buffer.Concat(data).ToArray();
            }

            if (behavior != EncodingServiceBufferBehavior.DoNothing)
            {
                cardCtx.Buffer = data;
            }
            if (!string.IsNullOrEmpty(Properties.CopyBufferToField))
            {
                cardCtx.UpdateFieldValue(GetCredentialFieldName(Properties.CopyBufferToField), cardCtx.Buffer);
            }
        }

        /// <summary>
        /// Get the credential field name on a format matching the service configuration.
        /// </summary>
        /// <param name="fieldName">The original field name.</param>
        /// <returns>The formatted field name for the service.</returns>
        protected string GetCredentialFieldName(string fieldName)
        {
            if (fieldName == null)
                throw new ArgumentNullException("fieldName");

            return (Properties.FieldNamePrefix ?? string.Empty) + fieldName;
        }
    }
}
