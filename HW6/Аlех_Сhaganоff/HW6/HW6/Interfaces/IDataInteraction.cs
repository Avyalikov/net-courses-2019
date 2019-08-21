using HW6.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW6.Interfaces
{
    public interface IDataInteraction
    {
        IContextProvider Context { get; set; }

        Portfolio AddPortfolio(int traderId, int shareId, int purchaseQuantity);
        
        Trader AddTrader(string firstName, string lastName, string phoneNumber, decimal balance = 0M);
        
        Transaction AddTransaction(int buyerId, int sellerId, int shareId, decimal sharePrice, int purchaseQuantity);
        
        void Dispose();
        
        List<int> GetAvailableSellers();
        
        List<int> GetAvailableShares(int traderId);
        
        int GetNumberOfTraders();
        
        Portfolio GetPortfolio(int traderId, int shareId);

        int GetPortfoliosCount(int traderId, int shareId);

        int GetShareCount(int shareId);

        decimal GetSharePrice(int shareId);

        int GetShareQuantityFromPortfoio(int traderId, int shareId);

        Trader GetTrader(int traderId);

        int GetTraderCount(int traderId);

        void RemovePortfolio(Portfolio portfolio);

        void RemoveTrader(Trader trader);

        void SaveChanges(); 
    }
}
