using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class WriteSDMFile(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.WriteSDMFile properties) : DESFireEV3Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.WriteSDMFile>(properties)
    {
        public override void Run(DESFireEV3Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            var ev3chip = (cardCtx.Chip as DESFireEV3Chip);
            var svc = ev3chip?.getService(LibLogicalAccess.CardServiceType.CST_NFC_TAG) as DESFireEV3NFCTag4CardService;

            if (svc == null)
                throw new EncodingException("Cannot retrieve the DESFire EV3 NFC Tag Type 4 card service.");

            svc.writeSDMFile(Properties.BaseUri ?? "https://card.leosac.com", Properties.ParamVcuid, Properties.ParamPicc, Properties.ParamReadCtr, Properties.ParamMac, Properties.IsoFIDNDEFFile);
        }
    }
}
