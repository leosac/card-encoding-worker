using Leosac.CredentialProvisioning.Core.Models;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Contracts
{
    public interface IReaderHub
    {
        Task<string> EncodeFromQueue(string templateId, string itemId, bool waitRemoval = true);

        Task<string> Encode(string templateId, WorkerCredentialBase credential, bool waitRemoval = true);
    }
}
