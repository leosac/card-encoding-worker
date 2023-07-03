using Leosac.CredentialProvisioning.Core.Contexts;

namespace Leosac.CredentialProvisioning.Encoding
{
    public abstract class EncodingDeviceContext : DeviceContext
    {
        public abstract Task<bool> Initialize();

        public abstract Task UnInitialize();

        public abstract Task<CardContext> PrepareCard();

        public abstract Task CompleteCard(CardContext context);
    }
}
