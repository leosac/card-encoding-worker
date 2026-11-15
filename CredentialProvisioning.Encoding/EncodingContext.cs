using Leosac.CredentialProvisioning.Core.Contexts;
using Leosac.CredentialProvisioning.Core.Models;
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

        public CredentialFragmentTemplateParameter[]? Parameters { get; set; }

        public T Resolve<T>(ParameterizedValue<T> value) where T : struct, IConvertible
        {
            var parameters = Parameters?.ToDictionary(p => p.Name, p => (object?)p.Value, StringComparer.Ordinal)
                ?? new Dictionary<string, object?>();
            return value.Resolve(parameters);
        }
    }
}
