using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateApplicationEV2(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateApplicationEV2 properties) : DESFireEV2Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateApplicationEV2>(properties)
    {
        public override void Run(DESFireEV2Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtxdeviceCtx)
        {
            cmd.createApplication(Properties.AID, (DESFireKeySettings)Properties.KeySettings, Properties.MaxNbKeys, (DESFireKeyType)Properties.KeyType, (FidSupport)Properties.FidSupport, Properties.IsoFID.GetValueOrDefault(0), Properties.IsoDFName?.ToByteVector() ?? [], Properties.NumberKeySets, Properties.MaxKeySize, Properties.ActualKeySetVersion, Properties.RollKeyNo, Properties.SpecificCapabilityData, Properties.SpecificVCKeys);
        }
    }
}
