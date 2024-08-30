using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.Mifare
{
    public class LoadKey : MifareAction<Leosac.CredentialProvisioning.Encoding.Chip.Mifare.LoadKey>
    {
        public LoadKey(Leosac.CredentialProvisioning.Encoding.Chip.Mifare.LoadKey properties) : base(properties)
        {

        }

        public override void Run(MifareCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            if (Properties.Key == null)
            {
                throw new EncodingException("A key must be defined.");
            }
            var key = encodingCtx.Keys?.Get(Properties.Key, cardCtx.Credential?.VolatileKeys);
            if (key == null)
            {
                throw new EncodingException("Cannot resolve the internal key reference.");
            }
            var llaKey = new MifareKey();
            CredentialKeyExt.SetKeyProperties(key, llaKey, cardCtx, Properties.Key?.Diversification);
            cmd.loadKey(Properties.KeyNo, (MifareKeyType)Properties.KeyType, llaKey);
        }
    }
}
