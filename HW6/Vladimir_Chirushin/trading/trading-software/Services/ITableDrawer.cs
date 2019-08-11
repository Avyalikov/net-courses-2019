using System.Linq;

namespace trading_software
{
    public interface ITableDrawer
    {
        void Show(IQueryable<Stock> Stocks);
        void Show(IQueryable<Client> Clients);
        void Show(IQueryable<Transaction> Transactions);
    }
}