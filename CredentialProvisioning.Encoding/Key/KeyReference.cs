using System.Text.Json.Serialization;

namespace Leosac.CredentialProvisioning.Encoding.Key
{
    public class KeyReference
    {
        public string KeyId { get; set; }

        public KeyDiversification? Diversification { get; set; }
    }
}
