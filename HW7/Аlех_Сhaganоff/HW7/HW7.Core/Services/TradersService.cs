using HW7.Core.Dto;
using HW7.Core.Models;
using HW7.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW7.Core.Services
{
    public class TradersService
    {
        private ITradersRepository tradersRepository;

        public TradersService(ITradersRepository tradersRepository)
        {
            this.tradersRepository = tradersRepository;
        }

        public int ReturnNumberOftraders()
        {
            return tradersRepository.GetNumberOfTraders();
        }

        public IQueryable<BalanceWithStatus> GetTraderBalanceWithStatus(int traderId)
        {
            return tradersRepository.GetTraderBalanceWithStatus(traderId);
        }

        public IQueryable<Trader> GetListOfSeveralTraders(int amountToSkip, int amountToTake)
        {
            return tradersRepository.GetListOfSeveralTraders(amountToSkip, amountToTake);
        }

        public Trader AddTrader(TraderToAdd trader)
        {
            if(trader == null || trader.FirstName == null || trader.FirstName.Length == 0 || trader.FirstName.Length > 100 || 
            trader.LastName == null || trader.LastName.Length == 0 || trader.LastName.Length > 100 ||
            (trader.PhoneNumber != null && trader.PhoneNumber.Length > 50))
            {
                return null;
            }

            return tradersRepository.AddTrader(trader);
        }

        public Trader UpdateTrader(TraderToUpdate trader)
        {
            if (trader == null || trader.FirstName == null || trader.FirstName.Length == 0 || trader.FirstName.Length > 100 ||
            trader.LastName == null || trader.LastName.Length == 0 || trader.LastName.Length > 100 ||
            (trader.PhoneNumber != null && trader.PhoneNumber.Length > 50))
            {
                return null;
            }

            return tradersRepository.UpdateTrader(trader);
        }

        public bool RemoveTrader(int id)
        {
            return tradersRepository.RemoveTrader(id);
        }

        public List<int> GetAvailableSellers()
        {
            return tradersRepository.GetAvailableSellers();
        }

        public List<int> GetAvailableBuyers()
        {
            return tradersRepository.GetAvailableBuyers();
        }
    }
}
