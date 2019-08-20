using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traiding.Core.Models;
using Traiding.Core.Repositories;

namespace Traiding.Core.Services
{
    public class BalancesService
    {
        private IBalanceTableRepository balanceTableRepository;

        public BalancesService(IBalanceTableRepository balanceTableRepository)
        {
            this.balanceTableRepository = balanceTableRepository;
        }

        public int RegisterNewBalance(BalanceRegistrationInfo args)
        {
            var entityToAdd = new BalanceEntity()
            {
                Client = args.Client,
                Amount = args.Amount,
                Status = args.Status
            };

            if (this.balanceTableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("Balance for this client has been registered. Can't continue.");
            }

            this.balanceTableRepository.Add(entityToAdd);

            this.balanceTableRepository.SaveChanges();

            return entityToAdd.Id;
        }

        public void ContainsById(int entityId)
        {
            if (!this.balanceTableRepository.ContainsById(entityId))
            {
                throw new ArgumentException("Can't find balance of client by this Id. May it has not been registered.");
            }
        }

        public BalanceEntity GetBalance(int entityId)
        {
            ContainsById(entityId);

            return this.balanceTableRepository.Get(entityId);
        }        

        public void ChangeBalance(int entityId, decimal newAmount)
        {
            ContainsById(entityId);

            this.balanceTableRepository.ChangeAmount(entityId, newAmount);
        }

        public IEnumerable<BalanceEntity> GetZeroBalances()
        {
            return this.balanceTableRepository.GetZeroBalances();
        }

        public IEnumerable<BalanceEntity> GetNegativeBalances()
        {
            return this.balanceTableRepository.GetNegativeBalances();
        }
    }
}
