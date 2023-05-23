using LibLogicalAccess.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leosac.CredentialProvisioning.Encoding.Worker.LLA.Chip.DESFire
{
    public class DeleteFile : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.DeleteFile>
    {
        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLADeviceContext deviceCtx)
        {
            cmd.deleteFile(Properties.FileNo);
        }
    }
}
