using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CommitTransaction(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CommitTransaction properties) : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CommitTransaction>(properties)
    {
        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.commitTransaction();
        }
    }
}
