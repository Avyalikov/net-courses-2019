using HW6.DataModel;
using HW6.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW6.Classes
{
    public class DataInteraction
    {
        private IContextProvider context;
        
        public DataInteraction(IContextProvider contextProvider)
        {
            this.context = contextProvider;
        }

        public Portfolio AddPortfolio(int traderId, int shareId, int purchaseQuantity)
        {
            return context.Portfolios.Add(new DataModel.Portfolio
            {
                TraderID = traderId,
                ShareId = shareId,
                Quantity = purchaseQuantity
            });
        }

        public Trader AddTrader(string firstName, string lastName, string phoneNumber, decimal balance = 0M)
        {
            return context.Traders.Add(new DataModel.Trader
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Balance = balance
            });
        }

        public Transaction AddTransaction(int buyerId, int sellerId, int shareId, decimal sharePrice, int purchaseQuantity)
        {
            return context.Transactions.Add(new DataModel.Transaction
            {
                BuyerId = buyerId,
                SellerId = sellerId,
                ShareId = shareId,
                PricePerShare = sharePrice,
                Quantity = purchaseQuantity,
                DateTime = DateTime.Now
            });
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public List<int> GetAvailableSellers()
        {
            return context.Portfolios.Where(p => p.Quantity > 0).Select(x => x.TraderID).Distinct().ToList();
        }

        public List<int> GetAvailableShares(int traderId)
        {
            return context.Portfolios.Where(p => p.TraderID == traderId).Select(x => x.ShareId).ToList();
        }

        public int GetNumberOfTraders()
        {
            return context.Traders.Count();
        }

        public Portfolio GetPortfolio(int traderId, int shareId)
        {
            return context.Portfolios.SingleOrDefault(p => p.TraderID == traderId && p.ShareId == shareId);
        }

        public int GetPortfoliosCount(int traderId, int shareId)
        {
            return context.Portfolios.Where(p => p.TraderID == traderId && p.ShareId == shareId).ToList().Count;
        }

        public int GetShareCount(int shareId)
        {
            return context.Shares.Where(t => t.ShareId == shareId).ToList().Count;
        }

        public decimal GetSharePrice(int shareId)
        {
            return context.Shares.Where(s => s.ShareId == shareId).Select(x => x.Price).FirstOrDefault();
        }

        public int GetShareQuantityFromPortfoio(int traderId, int shareId)
        {
            return context.Portfolios.Where(p => p.TraderID == traderId && p.ShareId == shareId).Select(x => x.Quantity).FirstOrDefault();
        }

        public Trader GetTrader(int traderId)
        {
            return context.Traders.SingleOrDefault(t => t.TraderId == traderId);
        }

        public int GetTraderCount(int traderId)
        {
            return context.Traders.Where(t => t.TraderId == traderId).ToList().Count;
        }

        public void RemovePortfolio(Portfolio portfolio)
        {
            context.Portfolios.Remove(portfolio);
        }

        public void RemoveTrader(Trader trader)
        {
            context.Traders.Remove(trader);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
