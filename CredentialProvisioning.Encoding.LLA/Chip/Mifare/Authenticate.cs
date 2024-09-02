using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.Mifare
{
    public class Authenticate(Leosac.CredentialProvisioning.Encoding.Chip.Mifare.Authenticate properties) : MifareAction<Leosac.CredentialProvisioning.Encoding.Chip.Mifare.Authenticate>(properties)
    {
        public override void Run(MifareCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.authenticate(Properties.Block, Properties.KeyNo, (MifareKeyType)Properties.KeyType);
        }
    }
}
