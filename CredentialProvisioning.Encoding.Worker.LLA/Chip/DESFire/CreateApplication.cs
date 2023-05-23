using LibLogicalAccess.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leosac.CredentialProvisioning.Encoding.Worker.LLA.Chip.DESFire
{
    public class CreateApplication : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateApplication>
    {
        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLADeviceContext deviceCtx)
        {
            cmd.createApplication(Properties.AID, (DESFireKeySettings)Properties.KeySettings, Properties.MaxNbKeys);
        }
    }
}
