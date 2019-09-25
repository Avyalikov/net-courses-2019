using Trading.Core.DataTransferObjects;

namespace Trading.Core
{
    public interface IClientsSharesService
    {
        int ChangeClientsSharesAmount(ClientsSharesInfo clientsSharesInfo);
    }
}