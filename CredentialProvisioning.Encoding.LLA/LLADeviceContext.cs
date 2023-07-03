namespace Leosac.CredentialProvisioning.Encoding.LLA
{
    public class LLADeviceContext : EncodingDeviceContext
    {
        public LibLogicalAccess.Reader.ISO7816ReaderUnit? ReaderUnit { get; set; }

        public override Task<bool> Initialize()
        {
            if (ReaderUnit == null)
                return Task.FromResult(false);

            return Task.FromResult(ReaderUnit.connectToReader());
        }

        public override Task UnInitialize()
        {
            if (ReaderUnit != null)
            {
                ReaderUnit.disconnectFromReader();
            }
            return Task.CompletedTask;
        }

        public override Task<CardContext> PrepareCard()
        {
            if (ReaderUnit == null)
                throw new EncodingException("Reader Unit is not defined.");

            if (!ReaderUnit.waitInsertion(0))
                throw new EncodingException("No card inserted.");

            if (!ReaderUnit.connect())
                throw new EncodingException("Cannot connect to the card.");

            return CreateCardContext();
        }

        public override Task CompleteCard(CardContext context)
        {
            if (context == null)
                throw new EncodingException("The card context is null.");

            if (ReaderUnit != null)
            {
                ReaderUnit.disconnect();
                ReaderUnit.waitRemoval(0);
            }
            return Task.CompletedTask;
        }

        protected Task<CardContext> CreateCardContext()
        {
            var context = new LLACardContext(this);
            context.Chip = ReaderUnit?.getSingleChip();
            return Task.FromResult<CardContext>(context);
        }
    }
}
