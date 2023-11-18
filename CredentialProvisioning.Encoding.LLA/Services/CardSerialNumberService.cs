using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public class CardSerialNumberService : EncodingService<Encoding.Services.CardSerialNumberService>
    {
        public CardSerialNumberService(Encoding.Services.CardSerialNumberService properties) : base(properties)
        {

        }

        public override void Run(CardContext cardCtx, KeyProvider? keystore, EncodingAction currentAction)
        {
            string? csn = null;
            if (cardCtx is LLACardContext llacardCtx)
            {
                var chip = llacardCtx.Chip;
                if (chip != null)
                {
                    var rawcsn = chip.getChipIdentifier();
                    if (rawcsn == null || rawcsn.Count < 1)
                    {
                        if (llacardCtx.LLADeviceContext.ReaderUnit is LibLogicalAccess.Reader.PCSCReaderUnit pcscReader)
                        {
                            rawcsn = pcscReader.getCardSerialNumber();
                            chip.setChipIdentifier(rawcsn);
                        }
                    }

                    if (rawcsn != null && rawcsn.Count > 0)
                    {
                        csn = Convert.ToHexString(rawcsn.ToArray());
                    }
                }
            }

            if (string.IsNullOrEmpty(csn))
            {
                throw new EncodingException("Cannot retrieve the Card Serial Number.");
            }
            
            var fieldName = GetCredentialFieldName("CSN");
            if (Properties.CheckCSN)
            {
                var data = cardCtx.Credential?.Data as IDictionary<string, object>;
                if (data != null && data.ContainsKey(fieldName))
                {
                    var expectedCsn = data[fieldName].ToString();
                    if (!string.IsNullOrEmpty(expectedCsn))
                    {
                        if (expectedCsn.ToString() != csn.ToLower())
                        {
                            throw new EncodingException("Unexpected card (CSN doesn't match).");
                        }
                    }
                }
            }
            cardCtx.UpdateFieldValue(fieldName, csn);

            HandleBuffer(cardCtx, null);
        }
    }
}
