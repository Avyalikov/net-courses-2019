namespace trading_software
{
    using System;
    using System.Collections.Generic;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Repositories;

    public class BlockOfSharesManager : IBlockOfSharesManager
    {
        private readonly IBlockOfSharesRepository blockOfSharesRepository;

        public BlockOfSharesManager(
            IBlockOfSharesRepository blockOfSharesRepository
            )
        {
            this.blockOfSharesRepository = blockOfSharesRepository;
        }


        public void AddShare(BlockOfShares blockOfShares)
        {
            blockOfSharesRepository.Insert(blockOfShares);
        }

        public bool IsClientHasStockType(int ClientID, int StockID)
        {
            throw new NotImplementedException();
        }

        public void ChangeShareAmountForClient(BlockOfShares blockOfShares)
        {
            throw new NotImplementedException();
        }

        public void ChangeSharePrice(BlockOfShares blockOfShares)
        {
            throw new NotImplementedException();
        }

        public int GetClientShareAmount(int ClientID, int StockID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BlockOfShares> ReadAllBlockOfShares()
        {
            IEnumerable<BlockOfShares> allShares = blockOfSharesRepository.GetAllBlockOfShares();
            return allShares;
        }
    }
}