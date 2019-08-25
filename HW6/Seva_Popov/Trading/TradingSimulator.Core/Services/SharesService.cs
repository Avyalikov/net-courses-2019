using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;

namespace TradingSimulator.Core.Services
{
    public class SharesService
    {
        private readonly ISharesTableRepository sharesTableRepository;

        public SharesService(ISharesTableRepository sharesTableRepository)
        {
            this.sharesTableRepository = sharesTableRepository;
        }

        public int RegisterNewShares(SharesRegistrationInfo args)
        {
            var entityToAdd = new SharesEntity() { Name = args.Name, Price = args.Price };

            if (this.sharesTableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("This shares has been registered. Can't continue");
            }

            this.sharesTableRepository.Add(entityToAdd);

            this.sharesTableRepository.SaveChanges();

            return entityToAdd.Id;
        }

        public SharesEntity GetShares(int sharesId)
        {
            if (!this.sharesTableRepository.ContainsById(sharesId))
            {
                throw new ArgumentException("Can't get shares by this id. May it has not been registred.");
            }

            return this.sharesTableRepository.Get(sharesId);
        }
    }
}

