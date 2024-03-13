using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class DeleteFile(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.DeleteFile properties) : DESFireAction<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.DeleteFile>(properties)
    {
        public override void Run(DESFireCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.deleteFile(Properties.FileNo);
        }
    }
}
