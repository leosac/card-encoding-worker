using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateApplicationEV1(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateApplicationEV1 properties) : DESFireEV1Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateApplicationEV1>(properties)
    {
        public override void Run(DESFireEV1Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtxdeviceCtx)
        {
            if (Properties.FidSupport == Encoding.Chip.DESFire.FidSupport.Enabled)
                cmd.createApplication(Properties.AID, (DESFireKeySettings)((byte)Properties.KeySettings | (Properties.ChangeKey << 4)), Properties.MaxNbKeys, (DESFireKeyType)Properties.KeyType, (FidSupport)Properties.FidSupport, Properties.IsoFID.GetValueOrDefault(0), Properties.IsoDFName?.ToByteVector() ?? []);
            else
                cmd.createApplication(Properties.AID, (DESFireKeySettings)((byte)Properties.KeySettings | (Properties.ChangeKey << 4)), Properties.MaxNbKeys, (DESFireKeyType)Properties.KeyType, (FidSupport)Properties.FidSupport);
        }
    }
}
