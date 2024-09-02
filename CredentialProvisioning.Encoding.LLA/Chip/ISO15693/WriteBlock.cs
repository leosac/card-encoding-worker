using LibLogicalAccess;
using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.ISO15693
{
    public class WriteBlock(Leosac.CredentialProvisioning.Encoding.Chip.ISO15693.WriteBlock properties) : ISO15693Action<Leosac.CredentialProvisioning.Encoding.Chip.ISO15693.WriteBlock>(properties)
    {
        public override void Run(ISO15693Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            if (cardCtx.Buffer == null || cardCtx.Buffer.Length == 0)
                throw new EncodingException("No data to write.");

            cmd.writeBlock(Properties.Block, new ByteVector(cardCtx.Buffer));
        }
    }
}
