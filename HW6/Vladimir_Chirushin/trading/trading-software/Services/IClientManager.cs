namespace trading_software
{
    public interface IClientManager
    {
        void AddNewClient();
        void AddClient(string name, string phoneNumber, decimal balance);
        void AddClient(Client client);
        void ReadAllClients();
        Client SelectRandom();
        // bool IsExist(Client client); is it need to be in interface?
    }
}