using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.Repositories;

namespace Trading.Core.Services
{
    public class BalanceService
    {
        private readonly IBalanceRepository balanceRepository;

        public BalanceService(IBalanceRepository balanceRepository)
        {
            this.balanceRepository = balanceRepository;
        }

        public void ChangeMoney(int id, decimal amount)
        {
            var client = balanceRepository.LoadClientByID(id);
            if (client == null)
            {
                return;
            }
            client.ClientBalance += amount;
            balanceRepository.SaveChanges();
        }

    }
}
