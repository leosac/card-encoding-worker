using Leosac.CredentialProvisioning.Encoding.Key;
using LibLogicalAccess;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public class ParseAccessControlDataService(Encoding.Services.ParseAccessControlDataService properties) : AccessControlDataService<Encoding.Services.ParseAccessControlDataService>(properties)
    {
        protected override void Run(CardContext cardCtx, KeyProvider? keystore, Format format)
        {
            if (cardCtx.Buffer == null)
            {
                throw new EncodingException("No access control data to parse.");
            }

            format.setLinearData(new ByteVector(cardCtx.Buffer));
            SyncCredentialDataWithFormat(cardCtx, format);
            HandleBuffer(cardCtx, null);
        }

        private void SyncCredentialDataWithFormat(CardContext cardCtx, Format format)
        {
            ArgumentNullException.ThrowIfNull(format);
            ArgumentNullException.ThrowIfNull(cardCtx?.Credential);

            var fieldNames = format.getValuesFieldList();
            foreach (var fieldName in fieldNames)
            {
                var credFieldName = GetCredentialFieldName(fieldName);
                string? v = null;
                var field = format.getFieldFromName(fieldName);
                if (field is StringDataField sf)
                {
                    //v = sf.getValue();
                    System.Text.Encoding? encoding = null;
                    var charset = sf.getCharset();
                    if (!string.IsNullOrEmpty(charset))
                    {
                        encoding = System.Text.Encoding.GetEncoding(charset);
                    }
                    encoding ??= System.Text.Encoding.UTF8;
                    v = encoding.GetString(sf.getRawValue().ToArray());
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

                if (v != null)
                {
                    cardCtx.UpdateFieldValue(credFieldName, v);
                }
            }
        }
    }
}
