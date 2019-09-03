namespace TradingApp.Core.Services
{
    using DTO;
    using Interfaces;
    using Models;
    using Repos;
    using System.Collections.Generic;
    using System.Linq;

    public class SharesService : ISharesService
    {
        private readonly ISharesRepository shareRepository;

        public SharesService(ISharesRepository shareRepository)
        {
            this.shareRepository = shareRepository;
        }

        public IEnumerable<Share> GetAllShares()
        {
            return shareRepository.GetAllShares();
        }

        public void RegisterShare(ShareRegistrationData shareData)
        {
            var newShare = new Share()
            {
                ShareType = shareData.ShareType,
                Price = shareData.SharePrice,
            };

            shareRepository.Insert(newShare);
            shareRepository.SaveChanges();
        }

        public int GetShareIDByType(string shareType)
        {
            return shareRepository.GetAllShares().Where(x => x.ShareType == shareType).FirstOrDefault().ShareID;
        }

        public string GetShareType(int shareID)
        {
            return shareRepository.GetAllShares().Where(x => x.ShareID == shareID).FirstOrDefault().ShareType;
        }

        public decimal GetSharePrice(int shareID)
        {
            return shareRepository.GetAllShares().Where(x => x.ShareID == shareID).FirstOrDefault().Price;
        }
    }
}
