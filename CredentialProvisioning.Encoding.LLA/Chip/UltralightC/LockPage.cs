using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.UltralightC
{
    public class LockPage(Leosac.CredentialProvisioning.Encoding.Chip.UltralightC.LockPage properties) : UltralightAction<Leosac.CredentialProvisioning.Encoding.Chip.UltralightC.LockPage>(properties)
    {
        public override void Run(MifareUltralightCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.lockPage(Properties.Page);
        }
    }
}
