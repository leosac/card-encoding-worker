using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class Authenticate : DESFireActionProperties
    {
        public override string Name => "Authenticate";

        public byte KeyNo { get; set; }

        public KeyReference? Key { get; set; }
    }
}
