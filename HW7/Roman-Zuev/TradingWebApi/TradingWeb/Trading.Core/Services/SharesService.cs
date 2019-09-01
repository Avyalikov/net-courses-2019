using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.Models;
using Trading.Core.Repositories;

namespace Trading.Core.Services
{
    public class SharesService : ISharesService
    {
        private readonly ISharesTableRepository sharesTableRepository;

        public SharesService(ISharesTableRepository sharesTableRepository)
        {
            this.sharesTableRepository = sharesTableRepository;
        }
        public void Add(SharesEntity sharesToAdd)
        {
            if (sharesToAdd.Price<=0 || sharesToAdd.SharesType.Length < 2)
            {
                throw new ArgumentException("Wrong data");
            }

            if (sharesTableRepository.Contains(sharesToAdd))
            {
                throw new ArgumentException("This shares type is already exists");
            }

            sharesTableRepository.Add(sharesToAdd);
            sharesTableRepository.SaveChanges();
        }

        public void Remove(SharesEntity sharesInfo)
        {
            if (!sharesTableRepository.Contains(sharesInfo))
            {
                throw new ArgumentException("This shares type doesn't exist");
            }
            var shareToRemove = sharesTableRepository.GetById(sharesInfo.Id);
            sharesTableRepository.Remove(shareToRemove);
            sharesTableRepository.SaveChanges();
        }

        public void Update(SharesEntity sharesInfo)
        {
            if (sharesInfo.Price <= 0 || sharesInfo.SharesType.Length < 2 || sharesInfo.Id == 0)
            {
                throw new ArgumentException("Wrong data");
            }

            if (!sharesTableRepository.Contains(sharesInfo))
            {
                throw new ArgumentException("This shares type doesn't exist");
            }
            var shareToUpdate = sharesTableRepository.GetById(sharesInfo.Id);
            shareToUpdate.SharesType = sharesInfo.SharesType;
            shareToUpdate.Price = sharesInfo.Price;
            sharesTableRepository.Update(shareToUpdate);
            sharesTableRepository.SaveChanges();
        }
    }
}
