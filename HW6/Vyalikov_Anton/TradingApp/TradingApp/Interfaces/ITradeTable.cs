namespace TradingApp.Interfaces
{
    using TradingApp.Core.Models;
    using System.Collections.Generic;

    interface ITradeTable
    {
        void Draw(IEnumerable<Shares> shares);
        void Draw(IEnumerable<Clients> clients);
        void Draw(IEnumerable<Transactions> transactions);
        void Draw(IEnumerable<ClientsPortfolios> portfolios);
    }
}

