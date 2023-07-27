using LibLogicalAccess;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public class ParseAccessControlDataService : AccessControlDataService<Encoding.Services.PrepareAccessControlDataService>
    {
        public ParseAccessControlDataService(Encoding.Services.PrepareAccessControlDataService properties) : base(properties)
        {

        }

        protected override void Run(CardContext cardCtx, Format format)
        {
            if (cardCtx.Buffer == null)
                throw new EncodingException("No access control data to parse.");
            SyncCredentialDataWithFormat(cardCtx, format);
            format.setLinearData(new ByteVector(cardCtx.Buffer));
            HandleBuffer(cardCtx, null);
        }

        private void SyncCredentialDataWithFormat(CardContext cardCtx, Format format)
        {
            if (format == null)
                throw new ArgumentNullException("format");

            if (cardCtx?.Credential == null)
                throw new ArgumentNullException("cardCtx.Credential");

            var fieldNames = format.getValuesFieldList();
            foreach (var fieldName in fieldNames)
            {
                var credFieldName = GetCredentialFieldName(fieldName);
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

                if (v != null)
                {
                    cardCtx.UpdateFieldValue(credFieldName, v);
                }
            }
        }
    }
}
