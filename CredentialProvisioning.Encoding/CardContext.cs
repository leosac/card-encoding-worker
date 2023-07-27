using Leosac.CredentialProvisioning.Core.Models;

namespace Leosac.CredentialProvisioning.Encoding
{
    /// <summary>
    /// The base card context.
    /// </summary>
    public abstract class CardContext
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="deviceContext">The associated device context.</param>
        /// <param name="credential">The associated credential details.</param>
        protected CardContext(EncodingDeviceContext deviceContext, WorkerCredentialBase? credential = null)
        {
            DeviceContext = deviceContext;
            Credential = credential;
            FieldsChanged = new List<string>();
        }

        /// <summary>
        /// The associated device context.
        /// </summary>
        public EncodingDeviceContext DeviceContext { get; private set; }

        /// <summary>
        /// The associated credential details.
        /// </summary>
        public WorkerCredentialBase? Credential { get; private set; }

        /// <summary>
        /// List of fields values changed since the initial credential details assignment.
        /// </summary>
        public IList<string> FieldsChanged { get; private set; }

        /// <summary>
        /// The shared temporary buffer which can be used for encoding actions / services raw data sharing.
        /// </summary>
        public byte[]? Buffer { get; set; }

        /// <summary>
        /// Update a field value from the credential details.
        /// </summary>
        /// <param name="fieldName">The targeted field name.</param>
        /// <param name="fieldValue">The new field value.</param>
        public void UpdateFieldValue(string fieldName, object fieldValue)
        {
            var data = Credential?.Data as IDictionary<string, object>;
            if (data != null)
            {
                if (data.ContainsKey(fieldName))
                {
                    if (fieldValue != null && data[fieldName]?.ToString() != fieldValue.ToString())
                    {
                        data[fieldName] = fieldValue;
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
