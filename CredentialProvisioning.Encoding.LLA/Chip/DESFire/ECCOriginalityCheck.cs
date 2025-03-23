using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class ECCOriginalityCheck(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ECCOriginalityCheck properties) : DESFireEV2Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ECCOriginalityCheck>(properties)
    {
        public override void Run(DESFireEV2Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            if (!cmd.performECCOriginalityCheck())
            {
                throw new EncodingException("NXP ECC Originality Check failed.");
            }
        }
    }
}
