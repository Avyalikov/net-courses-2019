using HW7.Core;
using HW7.Core.Models;
using HW7.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW7.Server.Repositories
{
    public class PortfoliosRepository : IPortfoliosRepository
    {
        private IContextProvider context;

        public PortfoliosRepository(IContextProvider context)
        {
            this.context = context;
        }

        public Portfolio GetPortfolio(int traderId, int shareId)
        {
            Portfolio portfolio = null;

            try
            {
                portfolio = context.Portfolios.Single(p => p.TraderID == traderId && p.ShareId == shareId);
            }
            catch
            {
                portfolio = null;
            }
            
            return portfolio;
        }

        public void RemovePortfolio(Portfolio portfolio)
        {
            context.Portfolios.Remove(portfolio);

            context.SaveChanges();
        }

        public int GetPortfoliosCount(int traderId, int shareId)
        {
            return context.Portfolios.Where(p => p.TraderID == traderId && p.ShareId == shareId).ToList().Count;
        }

        public Portfolio AddPortfolio(int traderId, int shareId, int purchaseQuantity)
        {
            var portfolio = context.Portfolios.Add(new Portfolio
            {
                TraderID = traderId,
                ShareId = shareId,
                Quantity = purchaseQuantity
            });

            context.SaveChanges();

            return portfolio;
        }

        public void AddShares(Portfolio portfolio, int quantity)
        {
            portfolio.Quantity += quantity;

            context.SaveChanges();
        }

        public void RemoveShares(Portfolio portfolio, int quantity)
        {
            portfolio.Quantity -= quantity;

            context.SaveChanges();
        }

        public int GetShareQuantityFromPortfoio(int traderId, int shareId)
        {
            return context.Portfolios.Where(p => p.TraderID == traderId && p.ShareId == shareId).Select(x => x.Quantity).FirstOrDefault();
        }
    }
}
