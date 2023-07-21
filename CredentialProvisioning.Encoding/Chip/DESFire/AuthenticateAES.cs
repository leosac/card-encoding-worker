using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class AuthenticateAES : DESFireActionProperties
    {
        public override string Name => "Authenticate AES";

        public byte KeyNo { get; set; }

        public KeyReference? Key { get; set; }
    }
}
