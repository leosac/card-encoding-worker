using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CommitReaderId(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CommitReaderId properties) : DESFireEV2Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CommitReaderId>(properties)
    {
        public override void Run(DESFireEV2Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.commitReaderID(Properties.ReaderId?.ToByteVector());
        }
    }
}
