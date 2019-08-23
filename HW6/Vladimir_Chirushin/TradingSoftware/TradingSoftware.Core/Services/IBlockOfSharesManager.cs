using System.Collections.Generic;
using TradingSoftware.Core.Models;

namespace trading_software
{
    public interface IBlockOfSharesManager
    {
        void AddShare(BlockOfShares blockOfShares);
        bool IsClientHasStockType(int ClientID, int StockID);
        void ChangeShareAmountForClient(BlockOfShares blockOfShares);
        void ChangeSharePrice(BlockOfShares blockOfShares);
        int GetClientShareAmount(int ClientID, int StockID);
        IEnumerable<BlockOfShares> ReadAllBlockOfShares();
    }
}