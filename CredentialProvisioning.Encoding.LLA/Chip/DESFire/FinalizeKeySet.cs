using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class FinalizeKeySet : DESFireEV2Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.FinalizeKeySet>
    {
        public FinalizeKeySet(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.FinalizeKeySet properties) : base(properties)
        {

        }

        public override void Run(DESFireEV2Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.finalizeKeySet(Properties.KeySetNo, Properties.KeySetVersion);
        }
    }
}
