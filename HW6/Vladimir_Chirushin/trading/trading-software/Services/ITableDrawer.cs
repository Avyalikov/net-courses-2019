namespace trading_software
{
    using System.Collections.Generic;

    public interface ITableDrawer
    {
        void Show(IEnumerable<Stock> Stocks);

        void Show(IEnumerable<Client> Clients);

        void Show(IEnumerable<Transaction> Transactions);

        void Show(IEnumerable<BlockOfShares> blockOfShares);
    }
}