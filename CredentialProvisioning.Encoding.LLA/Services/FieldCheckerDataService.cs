using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public class FieldCheckerDataService(Encoding.Services.FieldCheckerDataService properties) : EncodingService<Encoding.Services.FieldCheckerDataService>(properties)
    {
        public override void Run(CardContext cardCtx, KeyProvider? keystore, EncodingFragmentTemplateContent.FragmentTemplateProperty[]? templateProperties, EncodingAction currentAction)
        {
            if (!string.IsNullOrEmpty(Properties.Field))
            {
                var fieldValue = cardCtx.GetFieldValue(Properties.Field)?.ToString();
                if (Properties.Required && string.IsNullOrEmpty(fieldValue))
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
