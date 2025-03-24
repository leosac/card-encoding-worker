using Leosac.CredentialProvisioning.Encoding.Services.Ndef;

namespace Leosac.CredentialProvisioning.Encoding.Services
{
    /// <summary>
    /// Prepare NDEF message data encoding service, for further writing.
    /// </summary>
    public class PrepareNdefMessageDataService : EncodingServiceProperties
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PrepareNdefMessageDataService()
        {
            BufferBehavior = EncodingServiceBufferBehavior.Overwrite;
        }

        /// <summary>
        /// The NDEF records.
        /// </summary>
        public NdefRecord[] Records { get; set; } = [];
    }
}
