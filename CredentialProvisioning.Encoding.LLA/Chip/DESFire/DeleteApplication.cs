using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class DeleteApplication(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.DeleteApplication properties) : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.DeleteApplication>(properties)
    {
        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.deleteApplication(Properties.AID);
        }
    }
}
