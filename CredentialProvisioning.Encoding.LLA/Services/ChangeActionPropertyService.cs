using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public class ChangeActionPropertyService : EncodingService<Encoding.Services.ChangeActionPropertyService>
    {
        public ChangeActionPropertyService(Encoding.Services.ChangeActionPropertyService properties) : base(properties)
        {

        }

        public override void Run(CardContext cardCtx, KeyProvider? keystore, EncodingAction currentAction)
        {
            if (string.IsNullOrEmpty(Properties.PropertyName))
                throw new EncodingException("PropertyName is undefined.");

            if (string.IsNullOrEmpty(Properties.SourceField))
                throw new EncodingException("SourceField is undefined.");

            var propInfo = Properties.GetType().GetProperty(Properties.PropertyName, System.Reflection.BindingFlags.Public);
            if (propInfo == null)
                throw new EncodingException("The property to update cannot be found.");

            var data = cardCtx.Credential?.Data as IDictionary<string, object>;
            if (data != null)
            {
                if (!data.ContainsKey(Properties.SourceField))
                    throw new EncodingException("The source data field cannot be found.");

                object v = data[Properties.SourceField];
                if (v != null)
                {
                    if (propInfo.PropertyType == typeof(ulong) && v.GetType() == typeof(string))
                    {
                        ulong.TryParse(v.ToString(), out ulong n);
                        v = n;
                    }
                    else if (propInfo.PropertyType == typeof(long) && v.GetType() == typeof(string))
                    {
                        long.TryParse(v.ToString(), out long n);
                        v = n;
                    }
                    else if (propInfo.PropertyType == typeof(uint) && v.GetType() == typeof(string))
                    {
                        uint.TryParse(v.ToString(), out uint n);
                        v = n;
                    }
                    else if (propInfo.PropertyType == typeof(int) && v.GetType() == typeof(string))
                    {
                        int.TryParse(v.ToString(), out int n);
                        v = n;
                    }
                    else if (propInfo.PropertyType == typeof(ushort) && v.GetType() == typeof(string))
                    {
                        ushort.TryParse(v.ToString(), out ushort n);
                        v = n;
                    }
                    else if (propInfo.PropertyType == typeof(short) && v.GetType() == typeof(string))
                    {
                        short.TryParse(v.ToString(), out short n);
                        v = n;
                    }
                    else if (propInfo.PropertyType == typeof(byte) && v.GetType() == typeof(string))
                    {
                        byte.TryParse(v.ToString(), out byte n);
                        v = n;
                    }
                }

                propInfo.SetValue(currentAction, v);
            }
        }
    }
}
