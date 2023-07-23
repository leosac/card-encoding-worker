using Leosac.CredentialProvisioning.Encoding.Services;
using LibLogicalAccess;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public class ParseAccessControlDataService : AccessControlDataService<PrepareAccessControlDataServiceProperties>
    {
        protected override void Run(CardContext cardCtx, Format format)
        {
            if (cardCtx.Buffer == null)
                throw new EncodingException("No access control data to parse.");
            SyncCredentialDataWithFormat(cardCtx.Credential?.Data, format, cardCtx.FieldsChanged);
            format.setLinearData(new ByteVector(cardCtx.Buffer));
            HandleBuffer(cardCtx, null);
        }

        private void SyncCredentialDataWithFormat(IDictionary<string, object> credentialData, Format format, IList<string>? fieldsChanged)
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
                    string? v = null;
                    var field = format.getFieldFromName(fieldName);
                    if (field is StringDataField sf)
                    {
                        v = sf.getValue();
                    }
                    else if (field is NumberDataField nf)
                    {
                        v = nf.getValue().ToString();
                    }
                    else if (field is BinaryDataField bf)
                    {
                        var bv = bf.getValue()?.ToArray();
                        if (bv != null)
                        {
                            v = Convert.ToHexString(bv);
                        }
                    }

                    if (v != null && credentialData[credFieldName]?.ToString() != v)
                    {
                        credentialData[credFieldName] = v;
                        if (fieldsChanged != null && !fieldsChanged.Contains(credFieldName))
                            fieldsChanged.Add(credFieldName);
                    }
                }
            }
        }
    }
}
