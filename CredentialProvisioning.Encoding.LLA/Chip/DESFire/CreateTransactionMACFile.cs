using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateTransactionMACFile(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateTransactionMACFile properties) : DESFireEV2Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateTransactionMACFile>(properties)
    {
        public override void Run(DESFireEV2Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            if (Properties.Key == null)
            {
                throw new EncodingException("A key must be defined.");
            }
            var key = (encodingCtx.Keys?.Get(Properties.Key, cardCtx.Credential?.VolatileKeys)) ?? throw new EncodingException("Cannot resolve the internal key reference.");
            var desfireKey = key.CreateKey(cardCtx, Properties.Key?.Diversification) as DESFireKey ?? throw new EncodingException("The key must be of type DESFire.");
            cmd.createTransactionMACFile(Properties.FileNo, (EncryptionMode)Properties.EncryptionMode, Properties.AccessRights.ConvertForLLA(), desfireKey, Properties.KeyVersion);
        }
    }
}
