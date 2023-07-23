using LibLogicalAccess;
using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class WriteData : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.WriteData>
    {
        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            if (cardCtx.Buffer == null || cardCtx.Buffer.Length == 0)
                throw new EncodingException("No data to write.");

            cmd.writeData(Properties.FileNo, Properties.Offset, new ByteVector(cardCtx.Buffer), (LibLogicalAccess.Card.EncryptionMode)Properties.EncryptionMode);
        }
    }
}
