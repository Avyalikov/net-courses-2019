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
                Cost = args.Cost
            };

            if (this.shareTypeTableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("This share type has been registered. Can't continue.");
            }

            this.shareTypeTableRepository.Add(entityToAdd);

            this.shareTypeTableRepository.SaveChanges();

            return entityToAdd.Id;
        }
    }
}
