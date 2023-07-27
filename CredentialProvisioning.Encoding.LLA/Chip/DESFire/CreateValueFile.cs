using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateValueFile : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateValueFile>
    {
        public CreateValueFile(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateValueFile properties) : base(properties)
        {

        }

        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.createValueFile(Properties.FileNo, (EncryptionMode)Properties.EncryptionMode, Properties.AccessRights.ConvertForLLA(), Properties.LowerLimit, Properties.UpperLimit, Properties.InitialValue, Properties.LimitedCreditEnabled);
        }
    }
}
