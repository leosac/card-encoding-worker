﻿using Leosac.CredentialProvisioning.Encoding.Services;
using LibLogicalAccess;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public class CardSerialNumberService : EncodingService<CardSerialNumberServiceProperties>
    {
        public override void Run(CardContext cardCtx, EncodingAction currentAction)
        {
            string? csn = null;
            if (cardCtx is LLACardContext llacardCtx)
            {
                var rawcsn = llacardCtx.Chip?.getChipIdentifier()?.ToArray();
                if (rawcsn != null)
                {
                    csn = Convert.ToHexString(rawcsn);
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