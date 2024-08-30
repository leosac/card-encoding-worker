using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.Mifare
{
    public class Authenticate : MifareAction<Leosac.CredentialProvisioning.Encoding.Chip.Mifare.Authenticate>
    {
        public Authenticate(Leosac.CredentialProvisioning.Encoding.Chip.Mifare.Authenticate properties) : base(properties)
        {

        }

        public override void Run(MifareCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.authenticate(Properties.Block, Properties.KeyNo, (MifareKeyType)Properties.KeyType);
        }
    }
}
