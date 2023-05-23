using Leosac.CredentialProvisioning.Core.Models;

namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class AuthenticateAES : DESFireActionProperties
    {
        public override string Name => "Authenticate AES";

        public byte KeyNo { get; set; }

        public CredentialKey? Key { get; set; }
    }
}
