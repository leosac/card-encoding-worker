using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding
{
    /// <summary>
    /// Content definition for an encoding fragment template.
    /// </summary>
    public class EncodingFragmentTemplateContent
    {
        /// <summary>
        /// SAM properties.
        /// </summary>
        public class SAMProperties
        {
            /// <summary>
            /// The SAM unlock key number.
            /// </summary>
            public byte UnlockKeyNo { get; set; }

            /// <summary>
            /// The SAM unlock key details.
            /// </summary>
            public KeyReference? UnlockKey { get; set; }
        }

        /// <summary>
        /// Fragment template global property.
        /// </summary>
        public class FragmentTemplateProperty
        {
            /// <summary>
            /// The property name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// The property displayed name.
            /// </summary>
            /// <returns></returns>
            public string Label { get; set; }

            /// <summary>
            /// The property value.
            /// </summary>
            public object? Value { get; set; }
        }

        /// <summary>
        /// The first encoding action to execute.
        /// </summary>
        public EncodingActionProperties? FirstAction { get; set; }

        /// <summary>
        /// Force the card type to use (optional).
        /// </summary>
        public string? ForceCardType { get; set; }

        /// <summary>
        /// The optional SAM properties.
        /// </summary>
        public SAMProperties? SAM { get; set; }

        /// <summary>
        /// Fragment Template properties, which can be referenced by actions/services.
        /// </summary>
        public FragmentTemplateProperty[]? Properties { get; set; }
    }
}
