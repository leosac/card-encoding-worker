using Leosac.CredentialProvisioning.Core.Models;

namespace Leosac.CredentialProvisioning.Encoding
{
    public abstract class CardContext
    {
        protected CardContext(EncodingDeviceContext deviceContext, WorkerCredentialBase? credential = null)
        {
            DeviceContext = deviceContext;
            Credential = credential;
            FieldsChanged = new List<string>();
        }

        public EncodingDeviceContext DeviceContext { get; private set; }

        public WorkerCredentialBase? Credential { get; private set; }

        public IList<string> FieldsChanged { get; private set; }

        public byte[]? Buffer { get; set; }

        public void UpdateFieldValue(string fieldName, object v)
        {
            var data = Credential?.Data as IDictionary<string, object>;
            if (data != null)
            {
                if (data.ContainsKey(fieldName))
                {
                    if (v != null && data[fieldName]?.ToString() != v.ToString())
                    {
                        data[fieldName] = v;
                        if (!FieldsChanged.Contains(fieldName))
                        {
                            FieldsChanged.Add(fieldName);
                        }
                    }
                }
            }
        }
    }
}
