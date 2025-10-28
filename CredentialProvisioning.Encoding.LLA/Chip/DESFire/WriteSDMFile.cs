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
            if (!string.IsNullOrEmpty(Properties.NFCApplicationKey?.KeyId))
            {
                var key = (encodingCtx.Keys?.Get(Properties.NFCApplicationKey, cardCtx.Credential?.VolatileKeys)) ?? throw new EncodingException("Cannot resolve the internal key reference.");
                if (key.CreateKey(cardCtx, Properties.NFCApplicationKey?.Diversification) is not DESFireKey desfireKey)
                {
                    throw new EncodingException("The key must be of type DESFire.");
                }
                svc.setAppNewKey(desfireKey);
            }
            svc.writeSDMFile(Properties.BaseUri ?? "https://card.leosac.com", Properties.ParamVcuid, Properties.ParamPicc, Properties.ParamReadCtr, Properties.ParamMac, Properties.IsoFIDNDEFFile);
        }
    }
}
