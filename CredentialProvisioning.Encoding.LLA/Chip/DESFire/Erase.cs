using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class Erase(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.Erase properties) : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.Erase>(properties)
    {
        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.erase();
        }
    }
}
