using Leosac.CredentialProvisioning.Encoding.Key;
using LibLogicalAccess;
using System.Text;

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
                            //sf.setValue(v);
                            System.Text.Encoding? encoding = null;
                            var charset = sf.getCharset();
                            if (!string.IsNullOrEmpty(charset))
                            {
                                encoding = System.Text.Encoding.GetEncoding(charset);
                            }
                            if (encoding == null)
                            {
                                encoding = System.Text.Encoding.Default;
                            }
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
