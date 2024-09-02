using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.FeliCa
{
    public class RequestService(Leosac.CredentialProvisioning.Encoding.Chip.FeliCa.RequestService properties) : FeliCaAction<Leosac.CredentialProvisioning.Encoding.Chip.FeliCa.RequestService>(properties)
    {
        public override void Run(FeliCaCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cardCtx.Buffer = BitConverter.GetBytes(cmd.requestService(Properties.Code));
        }
    }
}
