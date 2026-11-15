using Leosac.CredentialProvisioning.Encoding.Key;

using Leosac.CredentialProvisioning.Core.Models;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public class FieldCheckerService(Encoding.Services.FieldCheckerService properties) : EncodingService<Encoding.Services.FieldCheckerService>(properties)
    {
        public override void Run(CardContext cardCtx, KeyProvider? keystore, CredentialFragmentTemplateParameter[]? parameters, EncodingAction currentAction)
        {
            if (!string.IsNullOrEmpty(Properties.Field))
            {
                var fieldValue = cardCtx.GetFieldValue(Properties.Field)?.ToString();
                if (Properties.IsDataRequired && string.IsNullOrEmpty(fieldValue))
                {
                    throw new EncodingException($"Field `{Properties.Field}` not defined.");
                }
                if (!string.IsNullOrEmpty(Properties.ExpectedValue))
                {
                    if (!string.Equals(fieldValue, Properties.ExpectedValue, StringComparison.OrdinalIgnoreCase))
                    {
                        if (!Properties.ReverseCheck)
                        {
                            throw new EncodingException($"Field `{Properties.Field}` value `{fieldValue}` does not match expected value `{Properties.ExpectedValue}`.");
                        }
                    }
                    else
                    {
                        if (Properties.ReverseCheck)
                        {
                            throw new EncodingException($"Field `{Properties.Field}` value `{fieldValue}` matches expected value `{Properties.ExpectedValue}` but reverse is set.");
                        }
                    }
                }
            }
        }
    }
}
