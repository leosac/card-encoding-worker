using Leosac.CredentialProvisioning.Encoding.Key;
using LibLogicalAccess;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public class PrepareAccessControlDataService : AccessControlDataService<Encoding.Services.PrepareAccessControlDataService>
    {
        public PrepareAccessControlDataService(Encoding.Services.PrepareAccessControlDataService properties) : base(properties)
        {

        }

        protected override void Run(CardContext cardCtx, KeyProvider? keystore, Format format)
        {
            SyncFormatWithCredentialData(format, cardCtx.Credential?.Data);
            var data = format.getLinearData();
            if (data == null)
                throw new EncodingException("Cannot compute the access control data.");
            HandleBuffer(cardCtx, data.ToArray());
        }

        private void SyncFormatWithCredentialData(Format format, IDictionary<string, object> credentialData)
        {
            if (format == null)
                throw new ArgumentNullException("format");

            if (credentialData == null)
                throw new ArgumentNullException("credentialData");

            var fieldNames = format.getValuesFieldList();
            foreach (var fieldName in fieldNames)
            {
                var credFieldName = GetCredentialFieldName(fieldName);
                if (credentialData.ContainsKey(credFieldName))
                {
                    var v = credentialData[credFieldName]?.ToString();
                    if (v != null)
                    {
                        var field = format.getFieldFromName(fieldName);
                        if (field is StringDataField sf)
                        {
                            sf.setValue(v);
                        }
                        else if (field is NumberDataField nf)
                        {
                            nf.setValue(long.Parse(v));
                        }
                        else if (field is BinaryDataField bf)
                        {
                            bf.setValue(new ByteVector(Convert.FromHexString(v)));
                        }
                    }
                }
            }
        }
    }
}
