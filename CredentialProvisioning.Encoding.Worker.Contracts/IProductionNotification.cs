using Leosac.CredentialProvisioning.Core.Models;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Contracts
{
    public interface IProductionNotification
    {
        Task NotifyError(string processId, string? message);

        Task NotifyProcessCompleted(string processId, ProvisioningState state);
    }
}
