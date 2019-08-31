using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.Repositories;

namespace Trading.Core.Services
{
    public enum BalanceZone{ Black, Orange, Green };
    public class BalanceService
    {
        private readonly IBalanceRepository balanceRepository;
        private readonly IValidator validator;

        public BalanceService(IBalanceRepository balanceRepository, IValidator validator)
        {
            this.balanceRepository = balanceRepository;
            this.validator = validator;
        }

        public void ChangeMoney(int id, decimal amount)
        {
            if (validator.ValidateClientMoney(id, amount))
            {
                var client = balanceRepository.LoadBalanceByID(id);
                if (client == null)
                {
                    return;
                }
                client.ClientBalance += amount;
                balanceRepository.SaveChanges();
            }
        }
        public BalanceZone GetBalanceZone(int id)
        {
            var balance = balanceRepository.LoadAllBalances().Where(x => x.ClientID == id).FirstOrDefault();
            if (balance.ClientBalance > 0)
                return BalanceZone.Green;
            if (balance.ClientBalance == 0)
                return BalanceZone.Orange;
            return BalanceZone.Black;
        }
        public BalanceEntity GetBalance(int ID)
        {
            return balanceRepository.LoadBalanceByID(ID);
        }

        public IEnumerable<BalanceEntity> GetClientsFromGreenZone()
        {
            return balanceRepository.LoadAllBalances().Where(x => x.ClientBalance > 0);
        }

        public IEnumerable<BalanceEntity> GetClientsFromOrangeZone()
        {
            return balanceRepository.LoadAllBalances().Where(x => x.ClientBalance == 0);
        }

        public IEnumerable<BalanceEntity> GetClientsFromBlackZone()
        {
            return balanceRepository.LoadAllBalances().Where(x => x.ClientBalance < 0);
        }
    }
}
