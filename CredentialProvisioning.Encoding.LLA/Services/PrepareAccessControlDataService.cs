using Leosac.CredentialProvisioning.Encoding.Key;
using LibLogicalAccess;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public class PrepareAccessControlDataService(Encoding.Services.PrepareAccessControlDataService properties) : AccessControlDataService<Encoding.Services.PrepareAccessControlDataService>(properties)
    {
        protected override void Run(CardContext cardCtx, KeyProvider? keystore, Format format)
        {
            SyncFormatWithCredentialData(format, cardCtx.Credential?.Data);
            var data = format.getLinearData() ?? throw new EncodingException("Cannot compute the access control data.");
            HandleBuffer(cardCtx, [.. data]);
        }

        private void SyncFormatWithCredentialData(Format format, IDictionary<string, object> credentialData)
        {
            ArgumentNullException.ThrowIfNull(format);
            ArgumentNullException.ThrowIfNull(credentialData);

            var fieldNames = format.getValuesFieldList();
            foreach (var fieldName in fieldNames)
            {
                var credFieldName = GetCredentialFieldName(fieldName);
                if (credentialData.TryGetValue(credFieldName, out object? value))
                {
                    var v = value?.ToString();
                    if (v != null)
                    {
                        var field = format.getFieldFromName(fieldName);
                        if (field is StringDataField sf)
                        {
                            //sf.setValue(v);
                            System.Text.Encoding? encoding = null;
                            var charset = sf.getCharset();
                            if (!string.IsNullOrEmpty(charset))
                            {
                                encoding = System.Text.Encoding.GetEncoding(charset);
                            }
                            encoding ??= System.Text.Encoding.UTF8;
                            sf.setRawValue(new ByteVector(encoding.GetBytes(v)));
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
