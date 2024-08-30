using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.Mifare
{
    public class ReadBinary : MifareAction<Leosac.CredentialProvisioning.Encoding.Chip.Mifare.ReadBinary>
    {
        public ReadBinary(Leosac.CredentialProvisioning.Encoding.Chip.Mifare.ReadBinary properties) : base(properties)
        {

        }

        public override void Run(MifareCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cardCtx.Buffer = cmd.readBinary(Properties.Block, Properties.Length)?.ToArray();
        }
    }
}
