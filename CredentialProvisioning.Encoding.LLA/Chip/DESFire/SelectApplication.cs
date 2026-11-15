using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class SelectApplication(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.SelectApplication properties) : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.SelectApplication>(properties)
    {
        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            Properties.AID = encodingCtx.Resolve(Properties.AID);
            cmd.selectApplication(Properties.AID);
        }
    }
}
