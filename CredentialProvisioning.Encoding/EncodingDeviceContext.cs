using Leosac.CredentialProvisioning.Core.Contexts;
using Leosac.CredentialProvisioning.Core.Models;

namespace Leosac.CredentialProvisioning.Encoding
{
    public abstract class EncodingDeviceContext : DeviceContext
    {
        public abstract Task<bool> Initialize();

        public abstract Task UnInitialize();

        public abstract Task<CardContext> PrepareCard(CredentialBase? credential = null);

        public abstract Task CompleteCard(CardContext context);
    }
}
