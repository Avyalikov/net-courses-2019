namespace trading_software
{
    public interface IClientManager
    {
        void ManualAddClient();
        void AddClient(string name, string phoneNumber, decimal balance);
        void AddClient(Client client);
        void ReadAllClients();
        int SelectRandomID();
        void BankruptRandomClient();
        void ChangeBalance(int ClientID, decimal accountGain);
        void ShowOrangeZone();
        void ShowBlackClients();
        void ReduceAssetsRandomClient();
    }
}