using Leosac.CredentialProvisioning.Core.Contexts;

namespace Leosac.CredentialProvisioning.Encoding
{
    public abstract class EncodingAction
    {
        public abstract void Run(CredentialContext<EncodingFragmentTemplateContent> encodingCtx, DeviceContext deviceCtx);
    }

    public abstract class EncodingAction<T> : EncodingAction where T : EncodingActionProperties, new()
    {
        public T Properties { get; set; } = new T();
    }
}
