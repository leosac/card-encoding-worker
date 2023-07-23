using Leosac.CredentialProvisioning.Core.Models;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Contracts
{
    public interface IReaderHub
    {
        Task<string> EncodeFromQueue(string templateId, string itemId);

        Task<string> Encode(string templateId, WorkerCredentialBase credential);
    }
}
