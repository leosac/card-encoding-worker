using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class AuthenticateAES : DESFireEV1Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.AuthenticateAES>
    {
        public override void RunDESFireEV1(DESFireEV1Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
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
            // Temporary hack until AuthenticateAES get overloaded with Key on argument list
            var crypto = (cmd.getChip() as DESFireEV1Chip).getCrypto();
            crypto.setKey(crypto.d_currentAid, 0, Properties.KeyNo, desfireKey);
            cmd.authenticateAES(Properties.KeyNo);
        }
    }
}
