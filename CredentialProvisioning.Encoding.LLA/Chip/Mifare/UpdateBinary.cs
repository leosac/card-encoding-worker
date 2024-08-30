using LibLogicalAccess;
using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.Mifare
{
    public class UpdateBinary : MifareAction<Leosac.CredentialProvisioning.Encoding.Chip.Mifare.UpdateBinary>
    {
        public UpdateBinary(Leosac.CredentialProvisioning.Encoding.Chip.Mifare.UpdateBinary properties) : base(properties)
        {

        }

        public override void Run(MifareCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            if (cardCtx.Buffer == null || cardCtx.Buffer.Length == 0)
                throw new EncodingException("No data to write.");

            cmd.updateBinary(Properties.Block, new ByteVector(cardCtx.Buffer));
        }
    }
}
