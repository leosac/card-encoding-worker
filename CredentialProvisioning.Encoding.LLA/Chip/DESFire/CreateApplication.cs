using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateApplication(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateApplication properties) : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateApplication>(properties)
    {
        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.createApplication(Properties.AID, (DESFireKeySettings)((byte)Properties.KeySettings | (Properties.ChangeKey << 4)), Properties.MaxNbKeys);
        }
    }
}
