using Leosac.CredentialProvisioning.Core.Models;

namespace Leosac.CredentialProvisioning.Encoding.LLA
{
    public class LLADeviceContext : EncodingDeviceContext
    {
        public LibLogicalAccess.Reader.ISO7816ReaderUnit? ReaderUnit { get; set; }

        public override Task<bool> Initialize()
        {
            return Task.Run(() =>
            {
                if (ReaderUnit == null)
                    return false;

                return ReaderUnit.connectToReader();
            });
        }

        public override Task UnInitialize()
        {
            return Task.Run(() =>
            {
                if (ReaderUnit != null)
                {
                    ReaderUnit.disconnectFromReader();
                }
            });
        }

        public override Task<CardContext> PrepareCard(WorkerCredentialBase? credential = null)
        {
            return Task.Run(() =>
            {
                if (ReaderUnit == null)
                    throw new EncodingException("Reader Unit is not defined.");

                if (!ReaderUnit.waitInsertion(0))
                    throw new EncodingException("No card inserted.");

                // We wait few ms for the chip to be really initialized / on RFID field
                Thread.Sleep(200);

                if (!ReaderUnit.connect())
                    throw new EncodingException("Cannot connect to the card.");

                return CreateCardContext(credential);
            });
        }

        public override Task CompleteCard(CardContext context)
        {
            if (context == null)
                throw new EncodingException("The card context is null.");

            return Task.Run(() =>
            {
                if (ReaderUnit != null)
                {
                    ReaderUnit.disconnect();
                    ReaderUnit.waitRemoval(0);
                }
            });
        }

        protected Task<CardContext> CreateCardContext(WorkerCredentialBase? credential = null)
        {
            return Task.Run<CardContext>(() =>
            {
                var context = new LLACardContext(this, credential);
                context.Chip = ReaderUnit?.getSingleChip();
                return context;
            });
        }
    }
}
