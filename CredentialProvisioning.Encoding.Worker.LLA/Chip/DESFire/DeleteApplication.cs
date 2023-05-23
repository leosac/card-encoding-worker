using LibLogicalAccess.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leosac.CredentialProvisioning.Encoding.Worker.LLA.Chip.DESFire
{
    public class DeleteApplication : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.DeleteApplication>
    {
        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLADeviceContext deviceCtx)
        {
            cmd.deleteApplication(Properties.AID);
        }
    }
}
