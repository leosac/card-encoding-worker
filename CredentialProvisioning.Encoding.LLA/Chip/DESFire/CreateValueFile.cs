using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateValueFile : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateValueFile>
    {
        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLADeviceContext deviceCtx)
        {
            cmd.createValueFile(Properties.FileNo, (EncryptionMode)Properties.EncryptionMode, Properties.AccessRights.ConvertForLLA(), Properties.LowerLimit, Properties.UpperLimit, Properties.InitialValue, Properties.LimitedCreditEnabled);
        }
    }
}
