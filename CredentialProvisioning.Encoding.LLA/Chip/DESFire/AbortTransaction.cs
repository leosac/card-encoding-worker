using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class AbortTransaction : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.AbortTransaction>
    {
        public AbortTransaction(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.AbortTransaction properties) : base(properties)
        {

        }

        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.abortTransaction();
        }
    }
}
