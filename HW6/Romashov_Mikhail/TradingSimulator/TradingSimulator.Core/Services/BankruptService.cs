using System;
using TradingSimulator.Core.Repositories;
using System.Collections.Generic;

namespace TradingSimulator.Core.Services
{
   
    public class BankruptService 
    {
        private readonly IBankruptRepository bankruptRepository;
       public BankruptService(IBankruptRepository bankruptRepository)
        {
            this.bankruptRepository = bankruptRepository;
        }

        public List<string> GetListTradersFromOrangeZone()
        {
            return bankruptRepository.GetTradersWithZeroBalance();
        }

        public List<string> GetListTradersFromBlackZone()
        {
            return bankruptRepository.GetTradersWithNegativeBalance();
        }
    }
}
