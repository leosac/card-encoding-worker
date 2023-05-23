using Leosac.CredentialProvisioning.Core.Models;

namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class SetConfiguration : DESFireActionProperties
    {
        public override string Name => "Set Configuration";

        public bool FormatCardEnabled { get; set; }

        public bool RandomIdEnabled { get; set; }

        public CredentialKey? DefaultKey { get; set; }
    }
}
