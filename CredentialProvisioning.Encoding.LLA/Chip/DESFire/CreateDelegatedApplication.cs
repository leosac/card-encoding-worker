using LibLogicalAccess;
using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateDelegatedApplication : DESFireEV2Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateDelegatedApplication>
    {
        public CreateDelegatedApplication(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateDelegatedApplication properties) : base(properties)
        {

        }

        public override void Run(DESFireEV2Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtxdeviceCtx)
        {
            var daminfo = new PairByteVectorByteVector();
            throw new NotImplementedException();
            //cmd.createDelegatedApplication(daminfo, Properties.AID, Properties.DAMSlotNo, Properties.DAMSlotVersion, Properties.QuotaLimit, (DESFireKeySettings)Properties.KeySettings, Properties.MaxNbKeys, (DESFireKeyType)Properties.KeyType, (FidSupport)Properties.FidSupport, Properties.IsoFID.GetValueOrDefault(0), Properties.IsoDFName?.ToByteVector(), Properties.NumberKeySets, Properties.MaxKeySize, Properties.ActualKeySetVersion, Properties.RollKeyNo, Properties.SpecificCapabilityData, Properties.SpecificVCKeys);
        }
    }
}
