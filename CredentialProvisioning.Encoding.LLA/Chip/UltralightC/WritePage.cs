﻿using LibLogicalAccess;
using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.UltralightC
{
    public class WritePage(Leosac.CredentialProvisioning.Encoding.Chip.UltralightC.WritePage properties) : UltralightAction<Leosac.CredentialProvisioning.Encoding.Chip.UltralightC.WritePage>(properties)
    {
        public override void Run(MifareUltralightCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            if (cardCtx.Buffer == null || cardCtx.Buffer.Length == 0)
                throw new EncodingException("No data to write.");

            cmd.writePage(Properties.Page, new ByteVector(cardCtx.Buffer));
        }
    }
}
