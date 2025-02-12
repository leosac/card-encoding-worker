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
                            var data = encoding.GetBytes(v);
                            if (data.Length * 8 > sf.getDataLength())
                            {
                                throw new ArgumentOutOfRangeException(string.Format("The field `{0}` value exceed the maximum size.", fieldName));
                            }
                            sf.setRawValue(new ByteVector(data));
                        }
                        else if (field is NumberDataField nf)
                        {
                            var data = ulong.Parse(v);
                            var bitlength = (int)Math.Log(data, 2);
                            if (bitlength > nf.getDataLength())
                            {
                                throw new ArgumentOutOfRangeException(string.Format("The field `{0}` value exceed the maximum size.", fieldName));
                            }
                            nf.setValue(data);
                        }
                        else if (field is BinaryDataField bf)
                        {
                            var data = Convert.FromHexString(v);
                            if (data.Length * 8 > bf.getDataLength())
                            {
                                throw new ArgumentOutOfRangeException(string.Format("The field `{0}` value exceed the maximum size.", fieldName));
                            }
                            bf.setValue(new ByteVector(data));
                        }
                    }
                }
            }
        }
    }
}
