using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traiding.Core.Dto;
using Traiding.Core.Models;
using Traiding.Core.Repositories;

namespace Traiding.Core.Services
{
    public class BalancesService
    {
        private IBalanceTableRepository tableRepository;

        public BalancesService(IBalanceTableRepository balanceTableRepository)
        {
            this.tableRepository = balanceTableRepository;
        }

        public int RegisterNewBalance(BalanceRegistrationInfo args)
        {
            var entityToAdd = new BalanceEntity()
            {
                Client = args.Client,
                Amount = args.Amount,
                Status = args.Status
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

        public IEnumerable<BalanceEntity> GetZeroBalances()
        {
            return this.tableRepository.GetZeroBalances();
        }

        public IEnumerable<BalanceEntity> GetNegativeBalances()
        {
            return this.tableRepository.GetNegativeBalances();
        }
    }
}
