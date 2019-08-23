using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSoftware.Core.Models;

namespace TradingSoftware.Core.Repositories
{
    public interface IBlockOfSharesRepository
    {
        void Insert(BlockOfShares blockOfShares);
        bool IsClientHasStockType(int ClientID, int StockID);
        int GetClientShareAmount(int ClientID, int StockID);
        void ChangeShareAmountForClient(BlockOfShares blockOfShares);
        IEnumerable<BlockOfShares> GetAllBlockOfShares();
    }
}
