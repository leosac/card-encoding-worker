using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class ChangeKey : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ChangeKey>
    {
        public ChangeKey(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ChangeKey properties) : base(properties)
        {

        }

        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            if (Properties.Key == null)
            {
                throw new EncodingException("A key must be defined.");
            }
            var key = encodingCtx.Keys?.Get(Properties.Key);
            if (key == null)
            {
                throw new EncodingException("Cannot resolve the internal key reference.");
            }
            var desfireKey = key.CreateKey(cardCtx, Properties.Key?.Diversification) as DESFireKey;
            if (desfireKey == null)
            {
                throw new EncodingException("The key must be of type DESFire.");
            }
            cmd.changeKey(Properties.KeyNo, desfireKey);
        }
    }
}
