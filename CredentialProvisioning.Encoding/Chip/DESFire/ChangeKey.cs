using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class ChangeKey : DESFireActionProperties
    {
        public override string Name => "Change Key";

        public byte KeyNo { get; set; }

        public KeyReference? Key { get; set; }
    }
}
