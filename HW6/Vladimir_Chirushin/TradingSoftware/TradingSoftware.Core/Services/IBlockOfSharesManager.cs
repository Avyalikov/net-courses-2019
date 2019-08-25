namespace TradingSoftware.Core.Services
{
    using System.Collections.Generic;
    using TradingSoftware.Core.Models;

    public interface IBlockOfSharesManager
    {
        void AddShare(BlockOfShares blockOfShares);

        bool IsClientHasStockType(int clientID, int shareID);

        void ChangeShareAmountForClient(BlockOfShares blockOfShares);

        int GetClientShareAmount(int clientID, int shareID);

        IEnumerable<BlockOfShares> GetAllBlockOfShares();
    }
}