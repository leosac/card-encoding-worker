using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class FinalizeKeySet(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.FinalizeKeySet properties) : DESFireEV2Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.FinalizeKeySet>(properties)
    {
        public override void Run(DESFireEV2Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.finalizeKeySet(Properties.KeySetNo, Properties.KeySetVersion);
        }
    }
}
