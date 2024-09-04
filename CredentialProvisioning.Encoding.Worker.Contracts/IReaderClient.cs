using Leosac.CredentialProvisioning.Core.Models;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Contracts
{
    public interface IReaderClient : IProductionNotification
    {
        Task<bool> ConnectToReader(string alias);

        Task DisconnectFromReader(string alias);

        Task<string> GetDeviceName(string alias);

        Task<string> GetDeviceType(string alias);

        Task SetCardType(string alias, string cardType);

        Task<string> CreateCardContext(string alias);

        Task<string> GetCardType(string alias, string contextId);

        Task<string> GetChipIdentifier(string alias, string contextId);

        Task<byte[]> SendRawCmd(string alias, string contextId, byte[] data);

        Task DestroyCardContext(string alias, string contextId);

        Task<bool> WaitCardInsertion(string alias,uint maxwait);

        Task<bool> ConnectToCard(string alias);

        Task<bool> IsConnectedToCard(string alias);

        Task DisconnectFromCard(string alias);

        Task<bool> WaitCardRemoval(string alias, uint maxwait);
    }
}
