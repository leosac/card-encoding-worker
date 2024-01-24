using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class InitializeKeySet : DESFireEV2Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.InitializeKeySet>
    {
        public InitializeKeySet(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.InitializeKeySet properties) : base(properties)
        {

        }

        public override void Run(DESFireEV2Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.initliazeKeySet(Properties.KeySetNo, (DESFireKeyType)Properties.KeyType);
        }
    }
}
