namespace trading_software
{
    public interface IBlockOfSharesManager
    {
        void AddNewShares();
        void AddShare(Client client, Stock stock, int amount);
        void AddShare(BlockOfShares blockOfShares);
        void ShowAllShares();
    }
}