﻿using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.FeliCa
{
    public class RequestResponse(Leosac.CredentialProvisioning.Encoding.Chip.FeliCa.RequestResponse properties) : FeliCaAction<Leosac.CredentialProvisioning.Encoding.Chip.FeliCa.RequestResponse>(properties)
    {
        public override void Run(FeliCaCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cardCtx.Buffer = [cmd.requestResponse()];
        }
    }
}