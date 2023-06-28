using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class Debit : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.Debit>
    {
        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLADeviceContext deviceCtx)
        {
            cmd.debit(Properties.FileNo, Properties.Value, (EncryptionMode)Properties.EncryptionMode);
        }
    }
}
