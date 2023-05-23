using LibLogicalAccess.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Leosac.CredentialProvisioning.Encoding.Worker.LLA.Chip.DESFire
{
    public class CreateCyclicRecordFile : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateCyclicRecordFile>
    {
        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLADeviceContext deviceCtx)
        {
            cmd.createCyclicRecordFile(Properties.FileNo, (EncryptionMode)Properties.EncryptionMode, Properties.AccessRights.ConvertForLLA(), Properties.RecordSize, Properties.MaxNumberOfRecords);
        }
    }
}
