using Leosac.CredentialProvisioning.Encoding.Key;
using Leosac.CredentialProvisioning.Encoding.Services;
using LibLogicalAccess;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public abstract class AccessControlDataService<T>(T properties) : EncodingService<T>(properties) where T: AccessControlDataService, new()
    {
        public override void Run(CardContext cardCtx, KeyProvider? keystore, EncodingAction currentAction)
        {
            var cfc = new CardsFormatComposite();
            var format = cfc.createFormatFromXml(Properties.Format, string.Empty) ?? throw new EncodingException("Cannot parse the access control format.");
            Run(cardCtx, keystore, format);
        }

        protected abstract void Run(CardContext cardCtx, KeyProvider? keystore, Format format);
    }
}
