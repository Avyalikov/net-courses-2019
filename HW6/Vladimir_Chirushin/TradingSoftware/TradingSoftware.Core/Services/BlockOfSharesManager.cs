namespace TradingSoftware.Core.Services
{
    using System.Collections.Generic;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Repositories;

    public class BlockOfSharesManager : IBlockOfSharesManager
    {
        private readonly IBlockOfSharesRepository blockOfSharesRepository;

        public BlockOfSharesManager(IBlockOfSharesRepository blockOfSharesRepository)
        {
            this.blockOfSharesRepository = blockOfSharesRepository;
        }

        public void AddShare(BlockOfShares blockOfShares)
        {
            this.blockOfSharesRepository.Insert(blockOfShares);
        }

        public bool IsClientHasStockType(int clientID, int shareID)
        {
            return this.blockOfSharesRepository.IsClientHasShareType(clientID, shareID);
        }

        public void ChangeShareAmountForClient(BlockOfShares blockOfShares)
        {
            this.blockOfSharesRepository.ChangeShareAmountForClient(blockOfShares);
        }

        public int GetClientShareAmount(int clientID, int shareID)
        {
            return this.blockOfSharesRepository.GetClientShareAmount(clientID, shareID);
        }

        public IEnumerable<BlockOfShares> GetAllBlockOfShares()
        {
            IEnumerable<BlockOfShares> allShares = this.blockOfSharesRepository.GetAllBlockOfShares();
            return allShares;
        }
    }
}