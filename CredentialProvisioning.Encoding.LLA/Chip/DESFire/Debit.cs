using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class Debit(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.Debit properties) : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.Debit>(properties)
    {
        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.debit(Properties.FileNo, Properties.Value, (EncryptionMode)Properties.EncryptionMode);
        }
    }
}
