﻿using Leosac.CredentialProvisioning.Encoding.Key;

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
        /// <param name="templateProperties">Template properties.</param>
        /// <param name="currentAction">The current encoding action from where the service run.</param>
        public abstract void Run(CardContext cardCtx, KeyProvider? keystore, EncodingFragmentTemplateContent.FragmentTemplateProperty[]? templateProperties, EncodingAction currentAction);
    }

    /// <summary>
    /// The generic base class for encoding service implementation.
    /// </summary>
    /// <typeparam name="T">The encoding service properties.</typeparam>
    /// <remarks>
    /// Base constructor.
    /// </remarks>
    /// <param name="properties">The associated encoding service properties.</param>
    public abstract class EncodingService<T>(T properties) : EncodingService where T : EncodingServiceProperties, new()
    {

        /// <summary>
        /// The associated encoding service properties.
        /// </summary>
        public T Properties { get; set; } = properties;

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
                    data = [.. data, .. cardCtx.Buffer];
            }
            if (behavior == EncodingServiceBufferBehavior.Append && cardCtx.Buffer != null)
            {
                if (data == null)
                    behavior = EncodingServiceBufferBehavior.DoNothing;
                else
                    data = [.. cardCtx.Buffer, .. data];
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
            ArgumentNullException.ThrowIfNull(fieldName);

            return (Properties.FieldNamePrefix ?? string.Empty) + fieldName;
        }
    }
}
