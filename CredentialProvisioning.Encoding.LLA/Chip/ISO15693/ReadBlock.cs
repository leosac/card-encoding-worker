using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.ISO15693
{
    public class ReadBlock(Leosac.CredentialProvisioning.Encoding.Chip.ISO15693.ReadBlock properties) : ISO15693Action<Leosac.CredentialProvisioning.Encoding.Chip.ISO15693.ReadBlock>(properties)
    {
        public override void Run(ISO15693Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cardCtx.Buffer = cmd.readBlock(Properties.Block)?.ToArray();
        }
    }
}
