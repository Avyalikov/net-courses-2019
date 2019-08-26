namespace Traiding.Core.Services
{
    using System;
    using Traiding.Core.Dto;
    using Traiding.Core.Models;
    using Traiding.Core.Repositories;

    public class SharesService
    {
        private IShareTableRepository tableRepository;

        public SharesService(IShareTableRepository shareTableRepository)
        {
            this.tableRepository = shareTableRepository;
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

            if (this.tableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("This share type has been registered. Can't continue.");
            }

            this.tableRepository.Add(entityToAdd);

            this.tableRepository.SaveChanges();

            return entityToAdd.Id;
        }

        public void ContainsById(int shareId)
        {
            if (!this.tableRepository.ContainsById(shareId))
            {
                throw new ArgumentException("Can't find share type by this Id. May it has not been registered.");
            }
        }

        public ShareEntity GetShare(int shareId)
        {
            ContainsById(shareId);

            return this.tableRepository.Get(shareId);
        }

        public void ChangeCompanyName(int shareId, string newCompanyName)
        {
            ContainsById(shareId);

            this.tableRepository.SetCompanyName(shareId, newCompanyName);

            this.tableRepository.SaveChanges();
        }

        public void ChangeType(int shareId, ShareTypeEntity newShareType)
        {
            ContainsById(shareId);

            this.tableRepository.SetType(shareId, newShareType);

            this.tableRepository.SaveChanges();
        }
    }
}
