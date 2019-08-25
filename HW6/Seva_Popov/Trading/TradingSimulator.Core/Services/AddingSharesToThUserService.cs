using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;

namespace TradingSimulator.Core.Services
{
    public class AddingSharesToThUserService
    { 
        private readonly IAddingSharesToThUserServiceTableRepository addingSharesToThUserServiceTableRepository;
        private readonly IUserTableRepository userTableRepository;
        private readonly ISharesTableRepository sharesTableRepository;

        public AddingSharesToThUserService(IAddingSharesToThUserServiceTableRepository addingSharesToThUserServiceTableRepository, IUserTableRepository userTableRepository, ISharesTableRepository sharesTableRepository)
        {
            this.addingSharesToThUserServiceTableRepository = addingSharesToThUserServiceTableRepository;
            this.userTableRepository = userTableRepository;
            this.sharesTableRepository = sharesTableRepository;
        }

        public void RegisterNewSharesToTheUser(AddingSharesToThUserEntity args)
        {
            var entityToAdd = new AddingSharesToThUserEntity() { UserId = args.UserId, ShareId = args.ShareId, AmountStocks = args.AmountStocks };

            
            if (this.sharesTableRepository.ContainsById(entityToAdd.ShareId) && this.userTableRepository.ContainsById(entityToAdd.UserId))
            {
                this.addingSharesToThUserServiceTableRepository.Add(entityToAdd);
            }

            this.addingSharesToThUserServiceTableRepository.SaveChanges();
        }
    }
}
