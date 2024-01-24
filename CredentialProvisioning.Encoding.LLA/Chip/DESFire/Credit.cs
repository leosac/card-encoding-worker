using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class Credit : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.Credit>
    {
        public Credit(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.Credit properties) : base(properties)
        {

        }

        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.credit(Properties.FileNo, Properties.Value, (EncryptionMode)Properties.EncryptionMode);
        }
    }
}
