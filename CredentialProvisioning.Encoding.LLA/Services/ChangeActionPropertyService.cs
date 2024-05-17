using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Encoding.Key;
using System.Text.Json;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public class ChangeActionPropertyService : EncodingService<Encoding.Services.ChangeActionPropertyService>
    {
        public ChangeActionPropertyService(Encoding.Services.ChangeActionPropertyService properties) : base(properties)
        {

        }

        public override void Run(CardContext cardCtx, KeyProvider? keystore, EncodingFragmentTemplateContent.FragmentTemplateProperty[]? templateProperties, EncodingAction currentAction)
        {
            if (string.IsNullOrEmpty(Properties.PropertyName))
                throw new EncodingException("PropertyName is undefined.");

            if (string.IsNullOrEmpty(Properties.SourceField) && string.IsNullOrEmpty(Properties.SourceProperty))
                throw new EncodingException("SourceField and SourceProperty are undefined.");

            var propInfo = currentAction.GetProperties().GetType().GetProperty(Properties.PropertyName);
            if (propInfo == null)
                throw new EncodingException("The property to update cannot be found.");

            object? v = null;
            if (!string.IsNullOrEmpty(Properties.SourceField))
            {
                var data = cardCtx.Credential?.Data as IDictionary<string, object>;
                if (data != null)
                {
                    if (!data.ContainsKey(Properties.SourceField))
                        throw new EncodingException("The source data field cannot be found.");

                    v = data[Properties.SourceField]?.ToString();
                }
            }
            else
            {
                if (templateProperties != null)
                {
                    var prop = templateProperties.SingleOrDefault(p => p.Name == Properties.SourceProperty);
                    if (prop == null)
                    {
                        throw new EncodingException("The template proprty cannot be found.");
                    }
                    v = prop.Value?.ToString();
                }
            }

            if (v != null)
            {
                if (propInfo.PropertyType == typeof(ulong))
                {
                    ulong.TryParse(v.ToString(), out ulong n);
                    v = n;
                }
                else if (propInfo.PropertyType == typeof(long))
                {
                    long.TryParse(v.ToString(), out long n);
                    v = n;
                }
                else if (propInfo.PropertyType == typeof(uint))
                {
                    uint.TryParse(v.ToString(), out uint n);
                    v = n;
                }
                else if (propInfo.PropertyType == typeof(int))
                {
                    int.TryParse(v.ToString(), out int n);
                    v = n;
                }
                else if (propInfo.PropertyType == typeof(ushort))
                {
                    ushort.TryParse(v.ToString(), out ushort n);
                    v = n;
                }
                else if (propInfo.PropertyType == typeof(short))
                {
                    short.TryParse(v.ToString(), out short n);
                    v = n;
                }
                else if (propInfo.PropertyType == typeof(byte))
                {
                    byte.TryParse(v.ToString(), out byte n);
                    v = n;
                }
            }

            propInfo.SetValue(currentAction.GetProperties(), v);
        }
    }
}
