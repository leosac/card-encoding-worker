using LibLogicalAccess;
using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.FeliCa
{
    public class Write(Leosac.CredentialProvisioning.Encoding.Chip.FeliCa.Write properties) : FeliCaAction<Leosac.CredentialProvisioning.Encoding.Chip.FeliCa.Write>(properties)
    {
        public override void Run(FeliCaCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            if (cardCtx.Buffer == null || cardCtx.Buffer.Length == 0)
                throw new EncodingException("No data to write.");

            cmd.write(Properties.Code, Properties.Block, new ByteVector(cardCtx.Buffer));
        }
    }
}
