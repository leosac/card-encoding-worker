using Leosac.CredentialProvisioning.Core.Models;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Contracts
{
    public interface IReaderHub
    {
        Task<string> EncodeFromQueue(Guid templateId, string itemId);

        Task<string> Encode(Guid templateId, CredentialBase credential);
    }
}
