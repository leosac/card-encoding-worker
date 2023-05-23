using Leosac.CredentialProvisioning.Core.Models;

namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class Authenticate : DESFireActionProperties
    {
        public override string Name => "Authenticate";

        public byte KeyNo { get; set; }

        public CredentialKey? Key { get; set; }
    }
}
