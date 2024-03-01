using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.UltralightC
{
    public class LockPage : UltralightAction<Leosac.CredentialProvisioning.Encoding.Chip.UltralightC.LockPage>
    {
        public LockPage(Leosac.CredentialProvisioning.Encoding.Chip.UltralightC.LockPage properties) : base(properties)
        {

        }

        public override void Run(MifareUltralightCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.lockPage(Properties.Page);
        }
    }
}
