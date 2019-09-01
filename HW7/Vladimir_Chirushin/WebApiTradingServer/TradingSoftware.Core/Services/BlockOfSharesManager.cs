namespace TradingSoftware.Core.Services
{
    using System.Collections.Generic;
    using TradingSoftware.Core.Dto;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Repositories;

    public class BlockOfSharesManager : IBlockOfSharesManager
    {
        private readonly IBlockOfSharesRepository blockOfSharesRepository;
        private readonly IClientRepository clientRepository;
        private readonly ISharesRepository shareRepository;

        public BlockOfSharesManager(
            IBlockOfSharesRepository blockOfSharesRepository,
            IClientRepository clientRepository,
            ISharesRepository shareRepository)
        {
            this.blockOfSharesRepository = blockOfSharesRepository;
            this.clientRepository = clientRepository;
            this.shareRepository = shareRepository;
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

        public ClientShares GetClientShares(int clientID)
        {
            var blockOfShares = this.blockOfSharesRepository.GetClientShares(clientID);
            var clientShares = new ClientShares();
            clientShares.clientName = this.clientRepository.GetClientName(clientID);
            foreach (var block in blockOfShares)
            {
                clientShares.ShareWithPrice.Add(
                    this.shareRepository.GetShareType(block.ShareID),
                    this.shareRepository.GetSharePrice(block.ShareID));
            }

            return clientShares;
        }

        public IEnumerable<BlockOfShares> GetAllBlockOfShares()
        {
            return this.blockOfSharesRepository.GetAllBlockOfShares();
        }

        public void UpdateClientShares(BlockOfShares blockOfShares)
        {
            if (this.IsClientHasStockType(blockOfShares.ClientID, blockOfShares.ShareID))
            {
                this.ChangeShareAmountForClient(blockOfShares);
            }
            else
            {
                this.AddShare(blockOfShares);
            }
        }

        public void Delete(BlockOfShares blockOfShare)
        {
            this.blockOfSharesRepository.Remove(blockOfShare);
        }
    }
}