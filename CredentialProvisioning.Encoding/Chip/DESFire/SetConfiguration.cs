using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class SetConfiguration : DESFireActionProperties
    {
        public override string Name => "Set Configuration";

        public bool FormatCardEnabled { get; set; }

        public bool RandomIdEnabled { get; set; }

        public KeyReference? DefaultKey { get; set; }
    }
}
