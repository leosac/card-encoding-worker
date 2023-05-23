using LibLogicalAccess.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leosac.CredentialProvisioning.Encoding.Worker.LLA.Chip.DESFire
{
    public class ChangeFileSettings : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.ChangeFileSettings>
    {
        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLADeviceContext deviceCtx)
        {
            cmd.changeFileSettings(Properties.FileNo, (LibLogicalAccess.Card.EncryptionMode)Properties.EncryptionMode, Properties.AccessRights.ConvertForLLA(), Properties.Plain);
        }
    }
}
