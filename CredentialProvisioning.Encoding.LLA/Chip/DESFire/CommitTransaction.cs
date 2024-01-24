using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CommitTransaction : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CommitTransaction>
    {
        public CommitTransaction(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CommitTransaction properties) : base(properties)
        {

        }

        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.commitTransaction();
        }
    }
}
