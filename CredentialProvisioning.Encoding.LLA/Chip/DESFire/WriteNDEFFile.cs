using LibLogicalAccess;
using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class WriteNDEFFile(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.WriteNDEFFile properties) : DESFireEV1Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.WriteNDEFFile>(properties)
    {
        public override void Run(DESFireEV1Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            var ev1chip = (cardCtx.Chip as DESFireEV1Chip);
            var svc = ev1chip?.getService(LibLogicalAccess.CardServiceType.CST_NFC_TAG) as DESFireEV1NFCTag4CardService;

            if (svc == null)
                throw new EncodingException("Cannot retrieve the DESFire EV1 NFC Tag Type 4 card service.");

            if (cardCtx.Buffer == null || cardCtx.Buffer.Length == 0)
                throw new EncodingException("No data to write.");

            svc.writeNDEFFile([.. cardCtx.Buffer], Properties.IsoFIDNDEFFile);
        }
    }
}
