using Leosac.CredentialProvisioning.Core;
using LibLogicalAccess;
using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class SetConfiguration : DESFireEV1Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.SetConfiguration>
    {
        public override void RunDESFireEV1(DESFireEV1Commands cmd, EncodingContext encodingCtx, LLADeviceContext deviceCtx)
        {
            if (Properties.DefaultKey != null)
            {
                var desfireKey = Properties.DefaultKey.CreateKey() as DESFireKey;
                if (desfireKey == null)
                {
                    throw new EncodingException("The key must be of type DESFire.");
                }
                cmd.setConfiguration(desfireKey);
            }
            else
            {
                cmd.setConfiguration(Properties.FormatCardEnabled, Properties.RandomIdEnabled);
            }
        }
    }
}
