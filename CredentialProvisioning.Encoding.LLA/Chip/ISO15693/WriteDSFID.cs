using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.ISO15693
{
    public class WriteDSFID(Leosac.CredentialProvisioning.Encoding.Chip.ISO15693.WriteDSFID properties) : ISO15693Action<Leosac.CredentialProvisioning.Encoding.Chip.ISO15693.WriteDSFID>(properties)
    {
        public override void Run(ISO15693Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.writeDSFID(Properties.DSFID);
        }
    }
}
