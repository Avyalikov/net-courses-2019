namespace trading_software
{
    using System.Collections.Generic;

    public interface IDataBaseDevice
    {
        void Add(BlockOfShares blockOfShares);
        bool Add(Client client);
        bool Add(Stock stock);
        bool Add(Transaction transaction);
        string GetClientName(int ClientID);
        int GetClientID(string ClientName);
        decimal GetClientBalance(int ClientID);
        string GetStockType(int StockID);
        int GetStockID(string StockType);
        int GetClientStockAmount(int ClientID, int StockID);
        int GetNumberOfClients();
        int GetNumberOfStocks();
        decimal GetStockPrice(int StockID);
        IEnumerable<Transaction> GetAllTransaction();
        IEnumerable<Client> GetAllClients();
        IEnumerable<Stock> GetAllStocks();
        IEnumerable<BlockOfShares> GetAllBlockOfShares();
        IEnumerable<Client> GetBlackClients();
        IEnumerable<Client> GetOrangeClients();
        bool IsClientExist(int ClientID);
        bool IsClientExist(string ClientName);
        bool IsStockExist(int StockID);
        bool IsStockExist(string StockType);
        bool IsClientHasStockType(int ClientID, int StockID);
        bool ChangeBalance(int ClientID, decimal accountGain);
        void BankruptClient(int ClientID);

    }
}