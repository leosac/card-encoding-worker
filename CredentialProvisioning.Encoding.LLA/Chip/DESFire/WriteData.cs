using LibLogicalAccess;
using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class WriteData(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.WriteData properties) : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.WriteData>(properties)
    {
        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            if (cardCtx.Buffer == null || cardCtx.Buffer.Length == 0)
                throw new EncodingException("No data to write.");

            cmd.writeData(Properties.FileNo, Properties.Offset, [.. cardCtx.Buffer], (LibLogicalAccess.Card.EncryptionMode)Properties.EncryptionMode);
        }
    }
}
