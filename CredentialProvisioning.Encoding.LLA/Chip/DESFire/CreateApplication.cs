using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateApplication : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateApplication>
    {
        public CreateApplication(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateApplication properties) : base(properties)
        {

        }

        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.createApplication(Properties.AID, (DESFireKeySettings)Properties.KeySettings, Properties.MaxNbKeys);
        }
    }
}
