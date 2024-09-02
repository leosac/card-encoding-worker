using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.ISO15693
{
    public class LockBlock(Leosac.CredentialProvisioning.Encoding.Chip.ISO15693.LockBlock properties) : ISO15693Action<Leosac.CredentialProvisioning.Encoding.Chip.ISO15693.LockBlock>(properties)
    {
        public override void Run(ISO15693Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.lockBlock(Properties.Block);
        }
    }
}
