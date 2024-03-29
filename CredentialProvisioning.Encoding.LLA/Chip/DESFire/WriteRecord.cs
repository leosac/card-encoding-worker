﻿using LibLogicalAccess;
using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class WriteRecord(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.WriteRecord properties) : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.WriteRecord>(properties)
    {
        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            if (cardCtx.Buffer == null || cardCtx.Buffer.Length == 0)
                throw new EncodingException("No data to write.");

            cmd.writeRecord(Properties.FileNo, Properties.Offset, new ByteVector(cardCtx.Buffer), (LibLogicalAccess.Card.EncryptionMode)Properties.EncryptionMode);
        }
    }
}
