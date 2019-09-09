namespace Traiding.Core.Services
{
    using System;
    using Traiding.Core.Dto;
    using Traiding.Core.Models;
    using Traiding.Core.Repositories;

    public class ShareTypesService
    {
        private IShareTypeTableRepository tableRepository;

        public ShareTypesService(IShareTypeTableRepository shareTypeTableRepository)
        {
            this.tableRepository = shareTypeTableRepository;
        }

        public int RegisterNewShareType(ShareTypeRegistrationInfo args)
        {
            var entityToAdd = new ShareTypeEntity()
            {
                Name = args.Name,
                Cost = args.Cost,
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

        public void ContainsById(int shareTypeId)
        {
            if (!this.tableRepository.ContainsById(shareTypeId))
            {
                throw new ArgumentException("Can't find share type by this Id. May it has not been registered.");
            }
        }

        public ShareTypeEntity GetShareType(int shareTypeId)
        {
            ContainsById(shareTypeId);

            return this.tableRepository.Get(shareTypeId);
        }

        public void ChangeName(int shareTypeId, string newName)
        {
            ContainsById(shareTypeId);

            this.tableRepository.SetName(shareTypeId, newName);

            this.tableRepository.SaveChanges();
        }

        public void ChangeCost(int shareTypeId, decimal newCost)
        {
            ContainsById(shareTypeId);

            this.tableRepository.SetCost(shareTypeId, newCost);

            this.tableRepository.SaveChanges();
        }

        
    }
}
