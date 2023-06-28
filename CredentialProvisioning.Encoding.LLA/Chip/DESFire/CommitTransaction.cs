using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CommitTransaction : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CommitTransaction>
    {
        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLADeviceContext deviceCtx)
        {
            cmd.commitTransaction();
        }
    }
}
