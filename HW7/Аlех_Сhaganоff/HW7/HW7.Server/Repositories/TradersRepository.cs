using HW7.Core;
using HW7.Core.Dto;
using HW7.Core.Models;
using HW7.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HW7.Server.Repositories
{
    public class TradersRepository : ITradersRepository
    {
        private IContextProvider context;

        public TradersRepository(IContextProvider context)
        {
            this.context = context;
        }

        public IContextProvider Context { get; set; }

        public int GetNumberOfTraders()
        {
            return context.Traders.Count();
        }

        public IQueryable<BalanceWithStatus> GetTraderBalanceWithStatus(int traderId)
        {
            return context.Traders.Where(x => x.TraderId == traderId).Select(x => new BalanceWithStatus
            { TraderId = x.TraderId, Balance = x.Balance, Status = x.Balance > 0 ? "green" : x.Balance == 0 ? "orange" : "black" });
        }

        public IQueryable<Trader> GetListOfSeveralTraders(int amountToSkip, int amountToTake)
        {
            return context.Traders.OrderBy(x => x.TraderId).Skip(amountToSkip).Take(amountToTake);
        }

        public Trader AddTrader(TraderToAdd trader)
        {
            var newTrader = context.Traders.Add(new Trader { FirstName = trader.FirstName,
                LastName = trader.LastName, PhoneNumber = trader.PhoneNumber, Balance = trader.Balance });

            context.SaveChanges();

            return newTrader;
        }

        public Trader UpdateTrader(TraderToUpdate trader)
        {
            var traderToChange = context.Traders.Find(trader.TraderId);

            if (traderToChange == null)
            {
                return null;
            }

            traderToChange.FirstName = trader.FirstName.Length == 0 ? traderToChange.FirstName : trader.FirstName;
            traderToChange.LastName = trader.LastName.Length == 0 ? traderToChange.LastName : trader.LastName;
            traderToChange.PhoneNumber = trader.PhoneNumber.Length == 0 ? traderToChange.PhoneNumber : trader.PhoneNumber;
            traderToChange.Balance = trader.Balance;
                        
            context.SaveChanges();

            return context.Traders.Find(trader.TraderId);
        }

        public bool RemoveTrader(int id)
        {
            var traderToRemove = context.Traders.Find(id);

            if(traderToRemove == null)
            {
                return false;
            }

            context.Traders.Remove(traderToRemove);

            context.SaveChanges();

            return true;
        }

        public Trader GetTrader(int traderId)
        {
            return context.Traders.Find(traderId);
        }

        public void AddToBalance(int traderId, decimal amount)
        {
            var traderToChange = context.Traders.Find(traderId);
            traderToChange.Balance += amount;

            context.SaveChanges();
        }

        public void SubtractFromBalance(int traderId, decimal amount)
        {
            var traderToChange = context.Traders.Find(traderId);
            traderToChange.Balance -= amount;

            context.SaveChanges();
        }

        public List<int> GetAvailableSellers()
        {
            return context.Portfolios.Where(p => p.Quantity > 0).Select(x => x.TraderID).Distinct().ToList();
        }

        public List<int> GetAvailableBuyers()
        {
            return context.Traders.Select(x => x.TraderId).Distinct().ToList();
        }
    }
}
