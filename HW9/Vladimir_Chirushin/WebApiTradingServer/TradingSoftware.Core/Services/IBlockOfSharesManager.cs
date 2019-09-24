namespace TradingSoftware.Core.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using TradingSoftware.Core.Dto;
    using TradingSoftware.Core.Models;

    public interface IBlockOfSharesManager
    {
        void AddShare(BlockOfShares blockOfShares);

        bool IsClientHasStockType(int clientID, int shareID);

        void ChangeShareAmountForClient(BlockOfShares blockOfShares);

        int GetClientShareAmount(int clientID, int shareID);

        IEnumerable<BlockOfShares> GetAllBlockOfShares();

        ClientShares GetClientShares(int clientID);

        void UpdateClientShares(BlockOfShares blockOfShares);

        void Delete(BlockOfShares blockOfShare);
    }
}