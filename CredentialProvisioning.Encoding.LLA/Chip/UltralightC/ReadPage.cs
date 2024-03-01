using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.UltralightC
{
    public class ReadPage : UltralightAction<Leosac.CredentialProvisioning.Encoding.Chip.UltralightC.ReadPage>
    {
        public ReadPage(Leosac.CredentialProvisioning.Encoding.Chip.UltralightC.ReadPage properties) : base(properties)
        {

        }

        public override void Run(MifareUltralightCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cardCtx.Buffer = cmd.readPage(Properties.Page)?.ToArray();
        }
    }
}
