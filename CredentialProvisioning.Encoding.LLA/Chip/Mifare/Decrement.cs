using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.Mifare
{
    public class Decrement : MifareAction<Leosac.CredentialProvisioning.Encoding.Chip.Mifare.Decrement>
    {
        public Decrement(Leosac.CredentialProvisioning.Encoding.Chip.Mifare.Decrement properties) : base(properties)
        {

        }

        public override void Run(MifareCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.decrement(Properties.Block, Properties.Value);
        }
    }
}
