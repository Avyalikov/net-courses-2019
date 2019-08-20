namespace trading_software
{
    using System.Collections.Generic;

    public interface IBlockOfSharesRepository
    {
        void Add(BlockOfShares blockOfShares);
        bool IsClientHasStockType(int ClientID, int StockID);
        int GetClientStockAmount(int ClientID, int StockID);
        IEnumerable<BlockOfShares> GetAllBlockOfShares();
    }
}