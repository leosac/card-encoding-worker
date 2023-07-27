using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class DeleteFile : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.DeleteFile>
    {
        public DeleteFile(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.DeleteFile properties) : base(properties)
        {

        }

        public override void RunDESFire(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.deleteFile(Properties.FileNo);
        }
    }
}
