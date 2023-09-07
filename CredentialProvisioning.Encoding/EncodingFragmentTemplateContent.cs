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
        /// The first encoding action to execute.
        /// </summary>
        public EncodingActionProperties? FirstAction { get; set; }

        /// <summary>
        /// The optional SAM properties.
        /// </summary>
        public SAMProperties? SAM { get; set; }
    }
}
