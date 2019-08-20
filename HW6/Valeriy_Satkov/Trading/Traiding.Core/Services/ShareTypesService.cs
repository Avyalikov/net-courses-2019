using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traiding.Core.Models;
using Traiding.Core.Repositories;

namespace Traiding.Core.Services
{
    public class ShareTypesService
    {
        private IShareTypeTableRepository shareTypeTableRepository;

        public ShareTypesService(IShareTypeTableRepository shareTypeTableRepository)
        {
            this.shareTypeTableRepository = shareTypeTableRepository;
        }

        public object RegisterNewShareType(ShareTypeRegistrationInfo args)
        {
            var entityToAdd = new ShareTypeEntity()
            {
                Name = args.Name,
                Cost = args.Cost,
                Status = args.Status
            };

            if (this.shareTypeTableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("This share type has been registered. Can't continue.");
            }

            this.shareTypeTableRepository.Add(entityToAdd);

            this.shareTypeTableRepository.SaveChanges();

            return entityToAdd.Id;
        }

        public ShareTypeEntity GetShareType(int shareTypeId)
        {
            if (!this.shareTypeTableRepository.ContainsById(shareTypeId))
            {
                throw new ArgumentException("Can't get client by this Id. May it has not been registered.");
            }

            return this.shareTypeTableRepository.Get(shareTypeId);
        }
    }
}
