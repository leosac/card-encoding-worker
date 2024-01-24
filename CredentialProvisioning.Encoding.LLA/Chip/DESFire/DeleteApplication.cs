using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class DeleteApplication : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.DeleteApplication>
    {
        public DeleteApplication(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.DeleteApplication properties) : base(properties)
        {

        }

        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.deleteApplication(Properties.AID);
        }
    }
}
