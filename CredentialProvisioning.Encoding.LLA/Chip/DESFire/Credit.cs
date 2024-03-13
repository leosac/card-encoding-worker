using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class Credit(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.Credit properties) : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.Credit>(properties)
    {
        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.credit(Properties.FileNo, Properties.Value, (EncryptionMode)Properties.EncryptionMode);
        }
    }
}
