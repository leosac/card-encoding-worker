using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class ChangeKeyEV2(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ChangeKeyEV2 properties) : DESFireEV2Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ChangeKeyEV2>(properties)
    {
        public override void Run(DESFireEV2Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            if (Properties.Key == null)
            {
                throw new EncodingException("A key must be defined.");
            }
            var key = (encodingCtx.Keys?.Get(Properties.Key, cardCtx.Credential?.VolatileKeys)) ?? throw new EncodingException("Cannot resolve the internal key reference.");
            if (key.CreateKey(cardCtx, Properties.Key?.Diversification) is not DESFireKey desfireKey)
            {
                throw new EncodingException("The key must be of type DESFire.");
            }

            if (Properties.OldKey != null)
            {
                var oldkey = (encodingCtx.Keys?.Get(Properties.OldKey, cardCtx.Credential?.VolatileKeys)) ?? throw new EncodingException("Cannot resolve the internal old key reference.");
                var desfireOldKey = oldkey.CreateKey(cardCtx, Properties.OldKey?.Diversification) as DESFireKey ?? throw new EncodingException("The old key must be of type DESFire.");
                var crypto = (cmd.getChip() as DESFireEV2Chip)?.getCrypto();
                crypto?.setKey(crypto.d_currentAid, Properties.KeySet, Properties.KeyNo, desfireOldKey);
            }

            cmd.changeKeyEV2(Properties.KeySet, Properties.KeyNo, desfireKey);
        }
    }
}
