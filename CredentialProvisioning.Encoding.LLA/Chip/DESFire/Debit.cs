using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class Debit : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.Debit>
    {
        public Debit(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.Debit properties) : base(properties)
        {

        }

        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.debit(Properties.FileNo, Properties.Value, (EncryptionMode)Properties.EncryptionMode);
        }
    }
}
