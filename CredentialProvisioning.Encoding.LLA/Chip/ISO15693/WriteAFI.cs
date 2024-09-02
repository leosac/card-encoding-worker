using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.ISO15693
{
    public class WriteAFI(Leosac.CredentialProvisioning.Encoding.Chip.ISO15693.WriteAFI properties) : ISO15693Action<Leosac.CredentialProvisioning.Encoding.Chip.ISO15693.WriteAFI>(properties)
    {
        public override void Run(ISO15693Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.writeAFI(Properties.AFI);
        }
    }
}
