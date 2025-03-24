using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class CreateNFCApplication(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateNFCApplication properties) : DESFireEV1Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateNFCApplication>(properties)
    {
        public override void Run(DESFireEV1Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            var ev1chip = (cardCtx.Chip as DESFireEV1Chip);
            var svc = ev1chip?.getService(LibLogicalAccess.CardServiceType.CST_NFC_TAG) as DESFireEV1NFCTag4CardService;

            if (svc == null)
            {
                throw new EncodingException("Cannot retrieve the DESFire EV1 NFC Tag Type 4 card service.");
            }

            svc.createNFCApplication(Properties.AID, Properties.IsoFIDApplication, Properties.IsoFIDCapabilityContainer, Properties.IsoFIDNDEFFile, Properties.NDEFFileSize);
        }
    }
}
