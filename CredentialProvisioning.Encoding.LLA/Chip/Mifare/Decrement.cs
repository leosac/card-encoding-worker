using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.Mifare
{
    public class Decrement(Leosac.CredentialProvisioning.Encoding.Chip.Mifare.Decrement properties) : MifareAction<Leosac.CredentialProvisioning.Encoding.Chip.Mifare.Decrement>(properties)
    {
        public override void Run(MifareCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.decrement(Properties.Block, Properties.Value);
        }
    }
}
