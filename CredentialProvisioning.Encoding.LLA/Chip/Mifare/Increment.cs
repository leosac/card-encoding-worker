using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.Mifare
{
    public class Increment : MifareAction<Leosac.CredentialProvisioning.Encoding.Chip.Mifare.Increment>
    {
        public Increment(Leosac.CredentialProvisioning.Encoding.Chip.Mifare.Increment properties) : base(properties)
        {

        }

        public override void Run(MifareCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.increment(Properties.Block, Properties.Value);
        }
    }
}
