using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateApplication : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateApplication>
    {
        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLADeviceContext deviceCtx)
        {
            cmd.createApplication(Properties.AID, (DESFireKeySettings)Properties.KeySettings, Properties.MaxNbKeys);
        }
    }
}
