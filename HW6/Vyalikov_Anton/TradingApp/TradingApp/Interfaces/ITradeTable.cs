namespace TradingApp.Interfaces
{
    using TradingApp.Core.Models;
    using System.Collections.Generic;

    interface ITradeTable
    {
        void Draw(IEnumerable<Share> shares);
        void Draw(IEnumerable<Client> clients);
        void Draw(IEnumerable<Transaction> transactions);
        void Draw(IEnumerable<ClientPortfolio> portfolios);
    }
}

