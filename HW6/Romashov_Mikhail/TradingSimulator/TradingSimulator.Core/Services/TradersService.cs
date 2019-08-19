using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;

namespace TradingSimulator.Core.Services
{
    public class TradersService
    {
        private readonly ITraderTableRepository traderTableRepository;
        public TradersService(ITraderTableRepository traderTableRepository)
        {
            this.traderTableRepository = traderTableRepository;
        }

        public int RegisterNewTrader(TraderInfo trader)
        {
            var entityToAdd = new TraderEntity()
            {
                CreatedAt = DateTime.Now,
                Name = trader.Name,
                Surname = trader.Surname,
                PhoneNumber = trader.PhoneNumber
            };

            if (traderTableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("This trader has been registered.");
            }
            traderTableRepository.Add(entityToAdd);

            traderTableRepository.SaveChanges();

            return entityToAdd.Id;
        }

       
        public TradersService()
        {
        }

        public TraderEntity GetTraders(int traderID)
        {
            if (!traderTableRepository.ContainsById(traderID))
            {
                throw new ArgumentException("Can`t get trader by this Id.");
            }
            return traderTableRepository.Get(traderID);
        }
    }
}
