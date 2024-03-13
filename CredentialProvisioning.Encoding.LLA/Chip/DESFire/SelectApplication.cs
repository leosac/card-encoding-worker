using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class SelectApplication(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.SelectApplication properties) : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.SelectApplication>(properties)
    {
        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.selectApplication(Properties.AID);
        }
    }
}
