using LibLogicalAccess.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leosac.CredentialProvisioning.Encoding.Worker.LLA.Chip.DESFire
{
    public class Credit : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.Credit>
    {
        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLADeviceContext deviceCtx)
        {
            cmd.credit(Properties.FileNo, Properties.Value, (EncryptionMode)Properties.EncryptionMode);
        }
    }
}
