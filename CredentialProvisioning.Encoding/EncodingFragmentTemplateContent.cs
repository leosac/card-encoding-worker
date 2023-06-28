using Leosac.CredentialProvisioning.Core.Models;

namespace Leosac.CredentialProvisioning.Encoding
{
    public class EncodingFragmentTemplateContent
    {
        public class SAMProperties
        {
            public byte UnlockKeyNo { get; set; }

            public CredentialKey? UnlockKey { get; set; }
        }

        public EncodingActionProperties? FirstAction { get; set; }

        public SAMProperties? SAM { get; set; }
    }
}
