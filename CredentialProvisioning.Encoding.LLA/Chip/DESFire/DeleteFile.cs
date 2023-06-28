using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class DeleteFile : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.DeleteFile>
    {
        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLADeviceContext deviceCtx)
        {
            cmd.deleteFile(Properties.FileNo);
        }
    }
}
