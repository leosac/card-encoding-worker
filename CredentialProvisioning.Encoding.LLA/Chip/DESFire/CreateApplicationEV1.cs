using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateApplicationEV1 : DESFireEV1Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateApplicationEV1>
    {
        public CreateApplicationEV1(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateApplicationEV1 properties) : base(properties)
        {

        }

        public override void RunDESFireEV1(DESFireEV1Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtxdeviceCtx)
        {
            if (Properties.FidSupport == Encoding.Chip.DESFire.FidSupport.FIDS_ISO_FID)
                cmd.createApplication(Properties.AID, (DESFireKeySettings)Properties.KeySettings, Properties.MaxNbKeys, (DESFireKeyType)Properties.KeyType, (FidSupport)Properties.FidSupport, Properties.IsoFID.GetValueOrDefault(0), Properties.IsoDFName?.ToByteVector());
            else
                cmd.createApplication(Properties.AID, (DESFireKeySettings)Properties.KeySettings, Properties.MaxNbKeys, (DESFireKeyType)Properties.KeyType, (FidSupport)Properties.FidSupport);
        }
    }
}
