using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class AbortTransaction : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.AbortTransaction>
    {
        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLADeviceContext deviceCtx)
        {
            cmd.abortTransaction();
        }
    }
}
