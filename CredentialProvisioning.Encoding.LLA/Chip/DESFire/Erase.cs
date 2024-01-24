using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class Erase : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.Erase>
    {
        public Erase(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.Erase properties) : base(properties)
        {

        }

        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.erase();
        }
    }
}
