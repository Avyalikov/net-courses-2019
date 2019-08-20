namespace trading_software
{
    public interface IBlockOfSharesManager
    {
        void ManualAddNewShare();

        void AddShare(int ClientID, int StockID, int Amount);

        void AddShare(BlockOfShares blockOfShares);

        void ShowAllShares();

        void CreateRandomShare();
    }
}