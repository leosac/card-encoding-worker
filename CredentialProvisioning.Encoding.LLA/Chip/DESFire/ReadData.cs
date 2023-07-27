using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class ReadData : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ReadData>
    {
        public ReadData(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ReadData properties) : base(properties)
        {
        }

        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cardCtx.Buffer = cmd.readData(Properties.FileNo, Properties.Offset, Properties.ByteLength, (LibLogicalAccess.Card.EncryptionMode)Properties.EncryptionMode)?.ToArray();
        }
    }
}
