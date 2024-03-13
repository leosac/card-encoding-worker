using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class ProximityCheck(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ProximityCheck properties) : DESFireEV2Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ProximityCheck>(properties)
    {
        public override void Run(DESFireEV2Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            DESFireKey? desfireKey = null;
            if (Properties.Key != null)
            {
                var key = (encodingCtx.Keys?.Get(Properties.Key, cardCtx.Credential?.VolatileKeys)) ?? throw new EncodingException("Cannot resolve the internal key reference.");
                desfireKey = key.CreateKey(cardCtx, Properties.Key?.Diversification) as DESFireKey;
                if (desfireKey == null)
                {
                    throw new EncodingException("The key must be of type DESFire.");
                }
            }

            cmd.proximityCheck(desfireKey, Properties.ChunkSize);
        }
    }
}
