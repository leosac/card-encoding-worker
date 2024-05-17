using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public class CardSerialNumberService(Encoding.Services.CardSerialNumberService properties) : EncodingService<Encoding.Services.CardSerialNumberService>(properties)
    {
        public override void Run(CardContext cardCtx, KeyProvider? keystore, EncodingFragmentTemplateContent.FragmentTemplateProperty[]? templateProperties, EncodingAction currentAction)
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

                            if (chip is LibLogicalAccess.Card.DESFireChip dfChip)
                            {
                                dfChip.getCrypto()?.setCryptoContext(rawcsn);
                            }
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
                var expectedCsn = cardCtx.GetFieldValue(fieldName)?.ToString();
                if (!string.IsNullOrEmpty(expectedCsn))
                {
                    if (!expectedCsn.Equals(csn, StringComparison.CurrentCultureIgnoreCase))
                    {
                        throw new EncodingException("Unexpected card (CSN doesn't match).");
                    }
                }
            }
            cardCtx.UpdateFieldValue(fieldName, csn);

            HandleBuffer(cardCtx, null);
        }
    }
}
