using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class DeleteApplication : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.DeleteApplication>
    {
        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.deleteApplication(Properties.AID);
        }
    }
}
