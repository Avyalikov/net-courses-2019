namespace HW7.Client
{
    public interface IRequestsProvider
    {
        string ConnectionString { get; set; }
        void AddClient();
        void AddShare();
        void CheckConnection(string url);
        void GetBalance();
        void GetListOfClients();
        void GetListOfShares();
        void GetTransactions();
        void MakeDeal((int sellerId, int buyerId, int shareId, int purchaseQuantity) data);
        void MakeDeal();
        void RemoveClient();
        void RemoveShare();
        void UpdateClient();
        void UpdateShare();
    }
}