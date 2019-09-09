namespace TradingApp.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TradingApp.Core.DTO;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;

    public class ShareService
    {
        private readonly IRepository<ShareTypeEntity> shareTypeTableRepository;
        private readonly IRepository<StockEntity> stockTableRepository;
        private readonly IRepository<TraderEntity> traderTableRepository;
        private readonly IRepository<ShareEntity> shareTableRepository;

        public ShareService(IRepository<ShareEntity> shareTableRepository, 
            IRepository<ShareTypeEntity> shareTypeTableRepository,
            IRepository<StockEntity> stockTableRepository,
            IRepository<TraderEntity> traderTableRepository)
        {
            this.shareTableRepository = shareTableRepository;
            this.shareTypeTableRepository = shareTypeTableRepository;
            this.stockTableRepository = stockTableRepository;
            this.traderTableRepository = traderTableRepository;
        }

        public virtual void ChangeShareType(int shareId, int shareTypeId)
        {
            ValidateShareExistence(shareId);
            ValidateShareTypeExistence(shareTypeId);
            ShareEntity shareToChange = this.shareTableRepository.GetById(shareId);
            ShareTypeEntity shareType = this.shareTypeTableRepository.GetById(shareTypeId);

            shareToChange.ShareType = shareType;

            this.shareTableRepository.Save(shareToChange);
        }
        public virtual List<ShareEntity> GetAllSharesByTraderId(int traderId)
        {
            ValidateTradersShareExistence(traderId);
            return this.shareTableRepository.GetAll()
                .Where(s => s.Owner.Id == traderId).ToList();
        }
        public virtual List<string> GetAllSharesListByTraderId(int traderId)
        {
            ValidateTradersShareExistence(traderId);
            return this.shareTableRepository.GetAll()
                .Where(s => s.Owner.Id == traderId)
                .Select(s => $"Owner: {s.Owner.FirstName} {s.Owner.LastName}, " +
                $"Company: {s.Stock.Company.Name}, " +
                $"Amount: {s.Amount}, " +
                $"Price: {s.Amount*s.ShareType.Multiplier*s.Stock.PricePerUnit: 0.00}")
                .ToList();
        }
        public virtual void AddNewShare(ShareInfo shareInfo)
        {
            ValidateStockExistence(shareInfo.StockId);
            ValidateOwnerExitence(shareInfo.OwnerId);
            ValidateShareTypeExistence(shareInfo.ShareTypeId);

            var shareToAdd = new ShareEntity
            {
                Owner = this.traderTableRepository.GetById(shareInfo.OwnerId),
                Amount = shareInfo.Amount,
                ShareType = this.shareTypeTableRepository.GetById(shareInfo.ShareTypeId),
                Stock = this.stockTableRepository.GetById(shareInfo.StockId)
            };

            this.shareTableRepository.Add(shareToAdd);

            this.shareTableRepository.SaveChanges();
        }
        public virtual List<ShareEntity> GetAllShares()
        { 
            return this.shareTableRepository.GetAll();
        }
        public virtual void UpdateShare(ShareInfo shareInfo)
        {
            ValidateShareExistence(shareInfo.Id);
            ValidateStockExistence(shareInfo.StockId);
            ValidateOwnerExitence(shareInfo.OwnerId);
            ValidateShareTypeExistence(shareInfo.ShareTypeId);

            var shareEntity = this.shareTableRepository.GetById(shareInfo.Id);

            shareEntity.Owner = this.traderTableRepository.GetById(shareInfo.OwnerId);
            shareEntity.ShareType = this.shareTypeTableRepository.GetById(shareInfo.ShareTypeId);
            shareEntity.Stock = this.stockTableRepository.GetById(shareInfo.StockId);
            shareEntity.Amount = shareInfo.Amount;

            this.shareTableRepository.Save(shareEntity);
        }
        public virtual void RemoveShare(int shareId)
        {
            ValidateShareExistence(shareId);

            var shareEntity = this.shareTableRepository.GetById(shareId);

            this.shareTableRepository.Delete(shareEntity);

            this.shareTableRepository.SaveChanges();
        }
        private void ValidateOwnerExitence(int ownerId)
        {
            if (this.traderTableRepository.GetById(ownerId) == null)
            {
                throw new Exception($"There's no user with given id in data source.");
            }
        }
        private void ValidateStockExistence(int stockId)
        {
            if (this.stockTableRepository.GetById(stockId) == null)
            {
                throw new Exception($"There's no stock with given id in data source.");
            }
        }
        private void ValidateTradersShareExistence(int traderId)
        {
            if (this.shareTableRepository.GetAll().Where(s => s.Owner.Id == traderId).Count() == 0)
            {
                throw new Exception("User doesnt have any shares.");
            }
        }
        private void ValidateShareTypeExistence(int shareTypeId)
        {
            if (this.shareTypeTableRepository.GetById(shareTypeId) == null)
            {
                throw new Exception("There is no share type with given Id.");
            }
        }
        private void ValidateShareExistence(int shareId)
        {
            if (this.shareTableRepository.GetById(shareId) == null)
            {
                throw new Exception("There is no share with given Id.");
            }
        }
    }
}
