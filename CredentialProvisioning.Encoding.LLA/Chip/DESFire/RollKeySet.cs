using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class RollKeySet : DESFireEV2Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.RollKeySet>
    {
        public RollKeySet(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.RollKeySet properties) : base(properties)
        {

        }

        public override void Run(DESFireEV2Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.rollKeySet(Properties.KeySetNo);
        }
    }
}
