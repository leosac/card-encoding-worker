using Leosac.CredentialProvisioning.Core.Models;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Contracts
{
    public interface IReaderHub
    {
        Task EncodeFromQueue(Guid templateId, string itemId);

        Task Encode(Guid templateId, CredentialBase credential);
    }
}
