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
            var items = bankruptRepository.GetTradersWithZeroBalance();

            List<string> listItems = new List<string>();
            foreach (var item in items)
            {
                listItems.Add(string.Concat(item.Name + " " + item.Surname));
            }
            return listItems;
        }

        public List<string> GetListTradersFromBlackZone()
        {
            var items = bankruptRepository.GetTradersWithNegativeBalance();
            List<string> listItems = new List<string>();
            foreach (var item in items)
            {
                listItems.Add(string.Concat(item.Name + " " + item.Surname));
            }
            return listItems;
        }
    }
}

