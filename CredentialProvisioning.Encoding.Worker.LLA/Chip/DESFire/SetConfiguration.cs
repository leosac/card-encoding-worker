using Leosac.CredentialProvisioning.Core;
using LibLogicalAccess;
using LibLogicalAccess.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leosac.CredentialProvisioning.Encoding.Worker.LLA.Chip.DESFire
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
