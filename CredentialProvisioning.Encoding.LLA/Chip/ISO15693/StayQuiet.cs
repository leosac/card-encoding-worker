using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.ISO15693
{
    public class StayQuiet(Leosac.CredentialProvisioning.Encoding.Chip.ISO15693.StayQuiet properties) : ISO15693Action<Leosac.CredentialProvisioning.Encoding.Chip.ISO15693.StayQuiet>(properties)
    {
        public override void Run(ISO15693Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.stayQuiet();
        }
    }
}
