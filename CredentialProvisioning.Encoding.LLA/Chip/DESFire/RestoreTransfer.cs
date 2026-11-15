using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public class RestoreTransfer(Leosac.CredentialProvisioning.Encoding.Chip.DESFire.RestoreTransfer properties) : DESFireEV2Action<Leosac.CredentialProvisioning.Encoding.Chip.DESFire.RestoreTransfer>(properties)
    {
        public override void Run(DESFireEV2Commands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            var targetFileNo = encodingCtx.Resolve(Properties.TargetFileNo);
            var sourceFileNo = encodingCtx.Resolve(Properties.SourceFileNo);
            cmd.restoreTransfer(targetFileNo, sourceFileNo);
        }
    }
}
