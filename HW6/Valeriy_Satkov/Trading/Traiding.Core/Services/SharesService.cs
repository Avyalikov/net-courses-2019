using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traiding.Core.Models;
using Traiding.Core.Repositories;

namespace Traiding.Core.Services
{
    public class SharesService
    {
        private IShareTableRepository shareTableRepository;

        public SharesService(IShareTableRepository shareTableRepository)
        {
            this.shareTableRepository = shareTableRepository;
        }

        public int RegisterNewShare(ShareRegistrationInfo args)
        {
            var entityToAdd = new ShareEntity()
            {
                CreatedAt = DateTime.Now,
                CompanyName = args.CompanyName,
                Type = args.Type,
                Status = args.Status
            };

            if (this.shareTableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("This share type has been registered. Can't continue.");
            }

            this.shareTableRepository.Add(entityToAdd);

            this.shareTableRepository.SaveChanges();

            return entityToAdd.Id;
        }

        public void ContainsById(int shareId)
        {
            if (!this.shareTableRepository.ContainsById(shareId))
            {
                throw new ArgumentException("Can't find share type by this Id. May it has not been registered.");
            }
        }

        public ShareEntity GetShare(int shareId)
        {
            ContainsById(shareId);

            return this.shareTableRepository.Get(shareId);
        }

        public void ChangeCompanyName(int shareId, string newCompanyName)
        {
            ContainsById(shareId);

            this.shareTableRepository.SetCompanyName(shareId, newCompanyName);
        }

        public void ChangeType(int shareId, ShareTypeEntity newShareType)
        {
            ContainsById(shareId);

            this.shareTableRepository.SetType(shareId, newShareType);
        }
    }
}
