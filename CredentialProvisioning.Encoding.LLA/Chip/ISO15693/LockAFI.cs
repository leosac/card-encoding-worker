using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.ISO15693
{
    public class LockAFI(Leosac.CredentialProvisioning.Encoding.Chip.ISO15693.LockAFI properties) : ISO15693Action<Leosac.CredentialProvisioning.Encoding.Chip.ISO15693.LockAFI>(properties)
    {
        public override void Run(ISO15693Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.lockAFI();
        }
    }
}
