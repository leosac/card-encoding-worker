using Leosac.CredentialProvisioning.Core.Contexts;
using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public abstract class DESFireAction<T> : EncodingAction<T> where T : Leosac.CredentialProvisioning.Encoding.Chip.DESFire.DESFireActionProperties, new()
    {
        public override void Run(CredentialContext<EncodingFragmentTemplateContent> encodingCtx, DeviceContext deviceCtx)
        {
            var llaCtx = deviceCtx as LLADeviceContext;
            if (llaCtx == null)
            {
                throw new EncodingException("Device context must be of type LLA");
            }

            var chip = llaCtx.Chip as DESFireChip;
            if (chip == null)
            {
                throw new EncodingException("Wrong chip type");
            }

            RunDESFire(chip.getDESFireCommands(), encodingCtx as EncodingContext, llaCtx);
        }

        public abstract void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLADeviceContext deviceCtx);
    }
}