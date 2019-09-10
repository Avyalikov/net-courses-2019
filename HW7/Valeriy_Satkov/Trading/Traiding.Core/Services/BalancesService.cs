namespace Traiding.Core.Services
{
    using System;
    using System.Collections.Generic;
    using Traiding.Core.Dto;
    using Traiding.Core.Models;
    using Traiding.Core.Repositories;

    public class BalancesService
    {
        private IBalanceTableRepository tableRepository;

        public BalancesService(IBalanceTableRepository balanceTableRepository)
        {
            this.tableRepository = balanceTableRepository;
        }

        public int RegisterNewBalance(BalanceRegistrationInfo args)
        {
            if (args.Amount <= 0)
            {
                throw new ArgumentException("Invalid BalanceRegistrationInfo. Can't continue.");
            }

            var entityToAdd = new BalanceEntity()
            {
                Client = args.Client,
                Amount = args.Amount,
                Status = true
            };

            if (this.tableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("Balance for this client has been registered. Can't continue.");
            }

            this.tableRepository.Add(entityToAdd);

            this.tableRepository.SaveChanges();

            return entityToAdd.Id;
        }

        public void ContainsById(int entityId)
        {
            if (!this.tableRepository.ContainsById(entityId))
            {
                throw new ArgumentException("Can't find balance of client by this Id. May it has not been registered.");
            }
        }

        public BalanceEntity GetBalance(int entityId)
        {
            ContainsById(entityId);

            return this.tableRepository.Get(entityId);
        }        

        public void ChangeBalance(int entityId, decimal newAmount)
        {
            ContainsById(entityId);

            this.tableRepository.ChangeAmount(entityId, newAmount);

            this.tableRepository.SaveChanges();
        }        
    }
}
