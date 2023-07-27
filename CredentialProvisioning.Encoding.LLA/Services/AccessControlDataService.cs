using Leosac.CredentialProvisioning.Encoding.Services;
using LibLogicalAccess;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public abstract class AccessControlDataService<T> : EncodingService<T> where T: AccessControlDataService, new()
    {
        protected AccessControlDataService(T properties) : base(properties)
        {

        }

        public override void Run(CardContext cardCtx, EncodingAction currentAction)
        {
            var cfc = new CardsFormatComposite();
            var format = cfc.createFormatFromXml(Properties.Format, string.Empty);
            if (format == null)
                throw new EncodingException("Cannot parse the access control format.");

            Run(cardCtx, format);
        }

        protected abstract void Run(CardContext cardCtx, Format format);
    }
}
