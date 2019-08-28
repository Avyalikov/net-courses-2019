using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.Repositories;
using Trading.Core.DataTransferObjects;

namespace Trading.Core.Services
{
    public class ShareService : IShareService
    {
        private readonly IShareRepository sharesRepository;

        public ShareService(IShareRepository sharesRepository)
        {
            this.sharesRepository = sharesRepository;
        }

        public int RegisterShare(ShareRegistrationInfo shareInfo)
        {
            var shareToAdd = new ShareEntity()
            {
                ShareName = shareInfo.Name,
                ShareCost = shareInfo.Cost
            };

            sharesRepository.Add(shareToAdd);
            sharesRepository.SaveChanges();

            return shareToAdd.ShareID;
        }

        public IEnumerable<ShareEntity> GetAllShares()
        {
            return sharesRepository.LoadAllShares();
        }
    }
}
