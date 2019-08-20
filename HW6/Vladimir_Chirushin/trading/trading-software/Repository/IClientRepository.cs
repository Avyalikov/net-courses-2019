namespace trading_software
{
    using System.Collections.Generic;

    public interface IClientRepository
    {
        bool Add(Client client);
        string GetClientName(int ClientID);
        int GetClientID(string ClientName);
        int GetNumberOfClients();
        decimal GetClientBalance(int ClientID);
        IEnumerable<Client> GetAllClients();
        IEnumerable<Client> GetBlackClients();
        IEnumerable<Client> GetOrangeClients();
        bool IsClientExist(int ClientID);
        bool IsClientExist(string ClientName);
        bool ChangeBalance(int ClientID, decimal accountGain);
        void BankruptClient(int ClientID);
    }
}