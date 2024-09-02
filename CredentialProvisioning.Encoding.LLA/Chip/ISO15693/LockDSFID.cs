using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.ISO15693
{
    public class LockDSFID(Leosac.CredentialProvisioning.Encoding.Chip.ISO15693.LockDSFID properties) : ISO15693Action<Leosac.CredentialProvisioning.Encoding.Chip.ISO15693.LockDSFID>(properties)
    {
        public override void Run(ISO15693Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.lockDSFID();
        }
    }
}
