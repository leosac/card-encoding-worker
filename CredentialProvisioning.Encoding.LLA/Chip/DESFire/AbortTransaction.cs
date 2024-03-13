using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class AbortTransaction(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.AbortTransaction properties) : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.AbortTransaction>(properties)
    {
        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.abortTransaction();
        }
    }
}
