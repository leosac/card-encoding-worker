namespace Leosac.CredentialProvisioning.Encoding
{
    public abstract class EncodingService
    {
        public abstract void Run(CardContext cardCtx, EncodingAction currentAction);
    }

    public abstract class EncodingService<T> : EncodingService where T : EncodingServiceProperties, new()
    {
        public T Properties { get; set; } = new T();

        protected void HandleBuffer(CardContext cardCtx, byte[]? data)
        {
            var behavior = Properties.BufferBehavior;
            if (behavior == EncodingServiceBufferBehavior.Prepend && cardCtx.Buffer != null)
            {
                if (data == null)
                    behavior = EncodingServiceBufferBehavior.DoNothing;
                else
                    data = data.Concat(cardCtx.Buffer).ToArray();
            }
            if (behavior == EncodingServiceBufferBehavior.Append && cardCtx.Buffer != null)
            {
                if (data == null)
                    behavior = EncodingServiceBufferBehavior.DoNothing;
                else
                    data = cardCtx.Buffer.Concat(data).ToArray();
            }

            if (behavior != EncodingServiceBufferBehavior.DoNothing)
            {
                cardCtx.Buffer = data;
            }
        }

        protected string GetCredentialFieldName(string fieldName)
        {
            if (fieldName == null)
                throw new ArgumentNullException("fieldName");

            return (Properties.FieldNamePrefix ?? string.Empty) + fieldName;
        }
    }
}
