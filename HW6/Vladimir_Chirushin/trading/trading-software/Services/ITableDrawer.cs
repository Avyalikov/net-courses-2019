using System.Collections.Generic;
using System.Linq;

namespace trading_software
{
    public interface ITableDrawer
    {
        void Show(IEnumerable<Stock> Stocks);
        void Show(IEnumerable<Client> Clients);
        void Show(IEnumerable<Transaction> Transactions);
        void Show(IEnumerable<BlockOfShares> blockOfShares);
    }
}