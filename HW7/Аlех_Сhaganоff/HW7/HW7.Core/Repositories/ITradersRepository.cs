using HW7.Core.Dto;
using HW7.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW7.Core.Repositories
{
    public interface ITradersRepository
    {
        int GetNumberOfTraders();
        IQueryable<BalanceWithStatus> GetTraderBalanceWithStatus(int traderId);

        IQueryable<Trader> GetListOfSeveralTraders(int amountToSkip, int amountToTake);

        Trader AddTrader(TraderToAdd trader);

        Trader UpdateTrader(TraderToUpdate trader);

        bool RemoveTrader(int id);

        Trader GetTrader(int traderId);

        void AddToBalance(int traderId, decimal amount);

        void SubtractFromBalance(int traderId, decimal amount);

        List<int> GetAvailableSellers();

        List<int> GetAvailableBuyers();
    }
}
