namespace TradingSoftware.ConsoleClient.Services
{
    using System.Collections.Generic;
    using TradingSoftware.Core.Models;

    public interface ITableDrawer
    {
        void Show(IEnumerable<Share> shares);

        void Show(IEnumerable<Client> clients);

        void Show(IEnumerable<Transaction> transactions);

        void Show(IEnumerable<BlockOfShares> blockOfShares);
    }
}