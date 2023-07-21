using Leosac.CredentialProvisioning.Core.Contexts;
using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding
{
    public class EncodingContext : CredentialContext<EncodingFragmentTemplateContent>
    {
        public KeyProvider? Keys { get; set; }
    }
}
