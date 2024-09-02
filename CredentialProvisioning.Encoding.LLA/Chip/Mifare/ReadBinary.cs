using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.Mifare
{
    public class ReadBinary(Leosac.CredentialProvisioning.Encoding.Chip.Mifare.ReadBinary properties) : MifareAction<Leosac.CredentialProvisioning.Encoding.Chip.Mifare.ReadBinary>(properties)
    {
        public override void Run(MifareCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cardCtx.Buffer = cmd.readBinary(Properties.Block, Properties.Length)?.ToArray();
        }
    }
}
