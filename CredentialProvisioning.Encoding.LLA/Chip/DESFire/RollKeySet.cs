using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class RollKeySet(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.RollKeySet properties) : DESFireEV2Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.RollKeySet>(properties)
    {
        public override void Run(DESFireEV2Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.rollKeySet(Properties.KeySetNo);
        }
    }
}
