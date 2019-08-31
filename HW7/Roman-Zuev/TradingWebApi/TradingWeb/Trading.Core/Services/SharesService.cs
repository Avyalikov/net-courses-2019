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
        public void AddNewShares(SharesEntity sharesToAdd)
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

        public void RemoveShares(SharesEntity sharesToRemove)
        {
            if (!sharesTableRepository.Contains(sharesToRemove))
            {
                throw new ArgumentException("This shares type doesn't exist");
            }

            sharesTableRepository.Remove(sharesToRemove);
            sharesTableRepository.SaveChanges();
        }

        public void UpdateShares(SharesEntity sharesToAdd)
        {
            if (sharesToAdd.Price <= 0 || sharesToAdd.SharesType.Length < 2)
            {
                throw new ArgumentException("Wrong data");
            }

            if (!sharesTableRepository.Contains(sharesToAdd))
            {
                throw new ArgumentException("This shares type doesn't exist");
            }

            sharesTableRepository.Update(sharesToAdd);
            sharesTableRepository.SaveChanges();
        }
    }
}
