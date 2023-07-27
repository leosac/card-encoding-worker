using Leosac.CredentialProvisioning.Core.Contexts;
using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding
{
    /// <summary>
    /// The encoding context.
    /// </summary>
    public class EncodingContext : CredentialContext<EncodingFragmentTemplateContent>
    {
        /// <summary>
        /// Previously loaded keys that can be referenced by templates.
        /// </summary>
        public KeyProvider? Keys { get; set; }
    }
}
