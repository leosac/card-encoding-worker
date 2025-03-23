using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class InitializeKeySet(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.InitializeKeySet properties) : DESFireEV2Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.InitializeKeySet>(properties)
    {
        public override void Run(DESFireEV2Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.initializeKeySet(Properties.KeySetNo, (DESFireKeyType)Properties.KeyType);
        }
    }
}
