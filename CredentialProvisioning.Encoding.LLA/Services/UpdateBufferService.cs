using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public class UpdateBufferService(Encoding.Services.UpdateBufferService properties) : EncodingService<Encoding.Services.UpdateBufferService>(properties)
    {
        public override void Run(CardContext cardCtx, KeyProvider? keystore, EncodingAction currentAction)
        {
            byte[]? data = null;
            if (!string.IsNullOrEmpty(Properties.FromField))
            {
                var fieldName = GetCredentialFieldName(Properties.FromField);
                data = cardCtx.GetBinaryFieldValue(fieldName);
            }

            if (Properties.IsDataRequired && (data == null || data.Length == 0))
            {
                throw new EncodingException("No data to update buffer from.");
            }

            HandleBuffer(cardCtx, data);
        }
    }
}
