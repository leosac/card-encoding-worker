using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class ChangeKey : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ChangeKey>
    {
        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLADeviceContext deviceCtx)
        {
            if (Properties.Key == null)
            {
                throw new EncodingException("A key must be defined.");
            }
            var desfireKey = Properties.Key.CreateKey() as DESFireKey;
            if (desfireKey == null)
            {
                throw new EncodingException("The key must be of type DESFire.");
            }
            cmd.changeKey(Properties.KeyNo, desfireKey);
        }
    }
}
