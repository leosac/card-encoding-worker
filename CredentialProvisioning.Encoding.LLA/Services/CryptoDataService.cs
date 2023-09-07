namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public class CryptoDataService : EncodingService<Encoding.Services.CryptoDataService>
    {
        public CryptoDataService(Encoding.Services.CryptoDataService properties) : base(properties)
        {

        }

        public override void Run(CardContext cardCtx, EncodingAction currentAction)
        {
            // TODO: implements
        }
    }
}
