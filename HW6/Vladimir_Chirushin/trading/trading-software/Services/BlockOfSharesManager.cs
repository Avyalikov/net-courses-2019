using System.Linq;

namespace trading_software
{
    public class BlockOfSharesManager : IBlockOfSharesManager
    {
        private readonly IInputDevice inputDevice;
        private readonly IOutputDevice outputDevice;
        private readonly ITableDrawer tableDrawer;
        public BlockOfSharesManager(IInputDevice inputDevice, IOutputDevice outputDevice, ITableDrawer tableDrawer)
        {
            this.inputDevice = inputDevice;
            this.outputDevice = outputDevice;
            this.tableDrawer = tableDrawer;
        }
        public void AddShare(Client client, Stock stock, int amount)
        {
            using (var db = new TradingContext())
            {
                var entry = db.BlockOfSharesTable
                    .Where(b => (b.ClienInBLock.ClientID == client.ClientID && b.StockInBlock.StockID == stock.StockID))
                    .FirstOrDefault();
                if(entry == null)
                {
                    var block = new BlockOfShares
                    {
                        ClienInBLock = client,
                        StockInBlock = stock,
                        NumberOfShares = amount
                    };
                        db.BlockOfSharesTable.Add(block);
                        db.SaveChanges();
                }
                else
                {
                    entry.NumberOfShares = entry.NumberOfShares + amount;
                }
                
            }
        }
        public void AddShare(BlockOfShares blockOfShares)
        {
            using (var db = new TradingContext())
            {
                var entry = db.BlockOfSharesTable
                    .Where(b => (b.ClienInBLock.ClientID == blockOfShares.ClienInBLock.ClientID && b.StockInBlock.StockID == blockOfShares.StockInBlock.StockID))
                    .FirstOrDefault();
                if (entry == null)
                {
                    db.BlockOfSharesTable.Add(blockOfShares);
                    db.SaveChanges();
                }
                else
                {
                    entry.NumberOfShares = entry.NumberOfShares + blockOfShares.NumberOfShares;
                }

            }
        }
        public void AddNewShares()
        {
            using (var db = new TradingContext())
            {
                outputDevice.WriteLine("Write Stock Type:");
                string stockNameInput = inputDevice.ReadLine();
                var stock = db.Stocks
                               .FirstOrDefault<Stock>(s => s.StockType == stockNameInput);

                outputDevice.WriteLine("Write client name:");
                string clientNameInput = inputDevice.ReadLine();
                var client = db.Clients
                               .FirstOrDefault<Client>(s => s.Name == clientNameInput);

                outputDevice.WriteLine("Write client name:");
                int amount = 0;
                while (true)
                {
                    if (int.TryParse(inputDevice.ReadLine(), out amount))
                        break;
                    else
                        outputDevice.WriteLine("Please enter valid amount.");
                }
                AddShare(client, stock, amount); 
            }
        }

        public void ShowAllShares()
        {
            using (var db = new TradingContext())
            {
                IQueryable<BlockOfShares> query = db.BlockOfSharesTable.AsQueryable<BlockOfShares>();
                tableDrawer.Show(query);
            }
        }
    }
}
