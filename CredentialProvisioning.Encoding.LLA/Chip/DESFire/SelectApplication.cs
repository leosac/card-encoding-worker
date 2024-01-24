using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class SelectApplication : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.SelectApplication>
    {
        public SelectApplication(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.SelectApplication properties) : base(properties)
        {

        }

        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.selectApplication(Properties.AID);
        }
    }
}
