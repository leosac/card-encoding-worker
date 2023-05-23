using LibLogicalAccess.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leosac.CredentialProvisioning.Encoding.Worker.LLA.Chip.DESFire
{
    public class CreateApplicationEV1 : DESFireEV1Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.CreateApplicationEV1>
    {
        public override void RunDESFireEV1(DESFireEV1Commands cmd, EncodingContext encodingCtx, LLADeviceContext deviceCtx)
        {
            if (Properties.FidSupport == Encoding.Chip.DESFire.FidSupport.FIDS_ISO_FID)
                cmd.createApplication(Properties.AID, (DESFireKeySettings)Properties.KeySettings, Properties.MaxNbKeys, (DESFireKeyType)Properties.KeyType, (FidSupport)Properties.FidSupport, Properties.IsoFID, Properties.IsoDFName?.ToByteVector());
            else
                cmd.createApplication(Properties.AID, (DESFireKeySettings)Properties.KeySettings, Properties.MaxNbKeys, (DESFireKeyType)Properties.KeyType, (FidSupport)Properties.FidSupport);
        }
    }
}
