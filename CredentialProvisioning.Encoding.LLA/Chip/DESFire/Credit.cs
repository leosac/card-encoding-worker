using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class Credit : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.Credit>
    {
        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.credit(Properties.FileNo, Properties.Value, (EncryptionMode)Properties.EncryptionMode);
        }
    }
}
