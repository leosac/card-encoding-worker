using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class Authenticate : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.Authenticate>
    {
        public Authenticate(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.Authenticate properties) : base(properties)
        {

        }

        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
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
            var desfireKey = key.CreateKey(cardCtx, Properties.Key?.Diversification) as DESFireKey;
            if (desfireKey == null)
            {
                throw new EncodingException("The key must be of type DESFire.");
            }
            
            cmd.authenticate(Properties.KeyNo, desfireKey);
        }
    }
}
