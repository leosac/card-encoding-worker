using Leosac.CredentialProvisioning.Core.Models;

namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class ChangeKey : DESFireActionProperties
    {
        public override string Name => "Change Key";

        public byte KeyNo { get; set; }

        public CredentialKey? Key { get; set; }
    }
}
