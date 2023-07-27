using Leosac.CredentialProvisioning.Core.Contexts;

namespace Leosac.CredentialProvisioning.Encoding
{
    /// <summary>
    /// The base class for encoding action implementation.
    /// </summary>
    public abstract class EncodingAction
    {
        /// <summary>
        /// Run the encoding action.
        /// </summary>
        /// <param name="encodingCtx">The encoding context.</param>
        /// <param name="cardCtx">The card context.</param>
        public abstract void Run(CredentialContext<EncodingFragmentTemplateContent> encodingCtx, CardContext cardCtx);
    }

    /// <summary>
    /// The generic base class for encoding action implementation.
    /// </summary>
    /// <typeparam name="T">The encoding action properties.</typeparam>
    public abstract class EncodingAction<T> : EncodingAction where T : EncodingActionProperties, new()
    {
        /// <summary>
        /// Base constructor.
        /// </summary>
        /// <param name="properties">The associated encoding action properties.</param>
        protected EncodingAction(T properties)
        {
            Properties = properties;
        }

        /// <summary>
        /// The associated encoding action properties.
        /// </summary>
        public T Properties { get; set; } = new T();
    }
}
