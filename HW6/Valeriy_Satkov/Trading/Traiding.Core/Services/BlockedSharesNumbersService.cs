using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traiding.Core.Dto;
using Traiding.Core.Repositories;
using Traiding.Core.Models;

namespace Traiding.Core.Services
{
    public class BlockedSharesNumbersService
    {
        private IBlockedSharesNumberTableRepository tableRepository;

        public BlockedSharesNumbersService(IBlockedSharesNumberTableRepository blockedSharesNumberTableRepository)
        {
            this.tableRepository = blockedSharesNumberTableRepository;
        }

        public int Create(BlockedSharesNumberRegistrationInfo args)
        {
            var entityToAdd = new BlockedSharesNumberEntity()
            {
                CreatedAt = DateTime.Now,
                ClientSharesNumber = args.ClientSharesNumber,
                Operation = args.Operation,
                Seller = args.ClientSharesNumber.Client,
                Share = args.Share,
                ShareTypeName = args.ShareTypeName,
                Cost = args.Cost,
                Number = args.Number
            };

            if (this.tableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("Blocked Shares Number with this data has been registered. Can't continue.");
            }

            this.tableRepository.Add(entityToAdd);

            this.tableRepository.SaveChanges();

            return entityToAdd.Id;
        }

        public void ContainsById(int entityId)
        {
            if (!this.tableRepository.ContainsById(entityId))
            {
                throw new ArgumentException("Can't find bloked shares number with this Id. May it has not been registered.");
            }
        }

        public object Get(int entityId)
        {
            ContainsById(entityId);

            return this.tableRepository.Get(entityId);
        }

        public void Remove(int entityId)
        {
            ContainsById(entityId);

            this.tableRepository.Remove(entityId);

            this.tableRepository.SaveChanges();
        }
    }
}
