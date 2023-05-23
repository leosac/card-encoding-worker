using Leosac.CredentialProvisioning.Core;
using LibLogicalAccess.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leosac.CredentialProvisioning.Encoding.Worker.LLA.Chip.DESFire
{
    public class Authenticate : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.Authenticate>
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
            
            cmd.authenticate(Properties.KeyNo, desfireKey);
        }
    }
}
