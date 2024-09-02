using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.FeliCa
{
    public class Read(Leosac.CredentialProvisioning.Encoding.Chip.FeliCa.Read properties) : FeliCaAction<Leosac.CredentialProvisioning.Encoding.Chip.FeliCa.Read>(properties)
    {
        public override void Run(FeliCaCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cardCtx.Buffer = cmd.read(Properties.Code, Properties.Block)?.ToArray();
        }
    }
}
