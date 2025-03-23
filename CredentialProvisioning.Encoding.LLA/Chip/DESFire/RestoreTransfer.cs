using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class RestoreTransfer(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.RestoreTransfer properties) : DESFireEV2Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.RestoreTransfer>(properties)
    {
        public override void Run(DESFireEV2Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            cmd.restoreTransfer(Properties.TargetFileNo, Properties.SourceFileNo);
        }
    }
}
