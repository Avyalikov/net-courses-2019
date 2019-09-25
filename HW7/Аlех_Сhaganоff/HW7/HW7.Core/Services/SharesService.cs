using HW7.Core.Dto;
using HW7.Core.Models;
using HW7.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW7.Core.Services
{
    public class SharesService
    {
        private ISharesRepository sharesRepository;

        public SharesService(ISharesRepository sharesRepository)
        {
            this.sharesRepository = sharesRepository;
        }

        public Share AddShare(ShareToAdd share)
        {
            if(share == null || share.Name == null || share.Name.Length == 0 || share.Name.Length > 100 || share.Price == 0M)
            {
                return null;
            }

            return sharesRepository.AddShare(share);
        }

        public IQueryable<ShareWithPrice> GetSharesWithPrice(int traderId)
        {
            return sharesRepository.GetSharesWithPrice(traderId);
        }

        public Share UpdateShare(ShareToUpdate share)
        {
            if (share == null || share.Name == null || share.Name.Length == 0 || share.Name.Length > 100 || share.Price == 0M)
            {
                return null;
            }

            return sharesRepository.UpdateShare(share);
        }

        public bool RemoveShare(int id)
        {
            return sharesRepository.RemoveShare(id);
        }

        public List<int> GetAvailableShares(int traderId)
        {
            return sharesRepository.GetAvailableShares(traderId);
        }
    }
}
