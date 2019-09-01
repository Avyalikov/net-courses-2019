using HW7.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW7.Core.Repositories
{
    public interface IPortfoliosRepository
    {
        Portfolio GetPortfolio(int traderId, int shareId);

        void RemovePortfolio(Portfolio portfolio);

        int GetPortfoliosCount(int traderId, int shareId);

        Portfolio AddPortfolio(int traderId, int shareId, int purchaseQuantity);

        void AddShares(Portfolio portfolio, int quantity);

        void RemoveShares(Portfolio portfolio, int quantity);

        int GetShareQuantityFromPortfoio(int traderId, int shareId);
    }
}
