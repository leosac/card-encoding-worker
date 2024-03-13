using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class ChangeKey(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ChangeKey properties) : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ChangeKey>(properties)
    {
        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            if (Properties.Key == null)
            {
                throw new EncodingException("A key must be defined.");
            }
            var key = (encodingCtx.Keys?.Get(Properties.Key, cardCtx.Credential?.VolatileKeys)) ?? throw new EncodingException("Cannot resolve the internal key reference.");
            var desfireKey = key.CreateKey(cardCtx, Properties.Key?.Diversification) as DESFireKey ?? throw new EncodingException("The key must be of type DESFire.");
            if (Properties.OldKey != null)
            {
                var oldkey = (encodingCtx.Keys?.Get(Properties.OldKey, cardCtx.Credential?.VolatileKeys)) ?? throw new EncodingException("Cannot resolve the internal old key reference.");
                var desfireOldKey = oldkey.CreateKey(cardCtx, Properties.OldKey?.Diversification) as DESFireKey ?? throw new EncodingException("The old key must be of type DESFire.");
                var crypto = (cmd.getChip() as DESFireEV1Chip)?.getCrypto();
                crypto?.setKey(crypto.d_currentAid, 0, Properties.KeyNo, desfireOldKey);
            }

            cmd.changeKey(Properties.KeyNo, desfireKey);
        }
    }
}
