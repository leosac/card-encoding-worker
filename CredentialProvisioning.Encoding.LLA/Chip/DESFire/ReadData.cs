using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class ReadData(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ReadData properties) : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ReadData>(properties)
    {
        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cardCtx.Buffer = cmd.readData(Properties.FileNo, Properties.Offset, Properties.ByteLength, (LibLogicalAccess.Card.EncryptionMode)Properties.EncryptionMode)?.ToArray();
        }
    }
}
