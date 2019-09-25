namespace TradingApp.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;

    public class ShareService
    {
        private readonly IRepository<ShareTypeEntity> shareTypeTableRepository;

        private readonly IRepository<ShareEntity> shareTableRepository;

        public ShareService(IRepository<ShareEntity> shareTableRepository, IRepository<ShareTypeEntity> shareTypeTableRepository)
        {
            this.shareTableRepository = shareTableRepository;
            this.shareTypeTableRepository = shareTypeTableRepository;
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

        public virtual List<ShareEntity> GetAllSharesByTraderId(int traderId)
        {
            ValidateTradersShareExistence(traderId);
            return this.shareTableRepository.GetAll().Where(s => s.Owner.Id == traderId).ToList();
        }

        private void ValidateTradersShareExistence(int traderId)
        {
            if (this.shareTableRepository.GetAll().Where(s => s.Owner.Id == traderId).Count() == 0)
            {
                throw new Exception("User doesnt have any shares.");
            }
        }
    }
}
