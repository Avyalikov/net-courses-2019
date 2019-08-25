namespace TradingSoftware.Core.Repositories
{
    using System.Collections.Generic;
    using TradingSoftware.Core.Models;

    public interface IBlockOfSharesRepository
    {
        void Insert(BlockOfShares blockOfShares);

        bool IsClientHasShareType(int clientID, int shareID);

        int GetClientShareAmount(int clientID, int shareID);

        void ChangeShareAmountForClient(BlockOfShares blockOfShares);

        IEnumerable<BlockOfShares> GetAllBlockOfShares();
    }
}