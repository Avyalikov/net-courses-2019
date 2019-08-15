using System;
using System.Linq;

namespace trading_software
{
    public class BlockOfSharesManager : IBlockOfSharesManager
    {
        private readonly IInputDevice inputDevice;
        private readonly IOutputDevice outputDevice;
        private readonly ITableDrawer tableDrawer;
        Random random = new Random();
        public BlockOfSharesManager(IInputDevice inputDevice, IOutputDevice outputDevice, ITableDrawer tableDrawer)
        {
            this.inputDevice = inputDevice;
            this.outputDevice = outputDevice;
            this.tableDrawer = tableDrawer;
        }
        public void AddShare(int ClientID, int StockID, int amount)
        {
            var block = new BlockOfShares
            {
                ClientID = ClientID,
                StockID = StockID,
                Amount = amount
            };
            AddShare(block);
        }

        public void AddShare(BlockOfShares blockOfShares)
        {
            using (var db = new TradingContext())
            {
                var entry = db.BlockOfSharesTable
                    .Where(b => (b.ClientID == blockOfShares.ClientID &&
                                 b.StockID == blockOfShares.StockID))
                    .FirstOrDefault();
                if (entry == null)
                {
                    db.BlockOfSharesTable.Add(blockOfShares);
                    db.SaveChanges();
                }
                else
                {
                    entry.Amount += blockOfShares.Amount;
                    db.SaveChanges();
                }

            }
        }
        public void ManualAddNewShare()
        {
            using (var db = new TradingContext())
            {
                outputDevice.WriteLine("Write Stock Type:");
                string stockNameInput = inputDevice.ReadLine();
                int stock = db.Stocks
                               .FirstOrDefault<Stock>(s => s.StockType == stockNameInput).StockID;

                outputDevice.WriteLine("Write client name:");
                string clientNameInput = inputDevice.ReadLine();
                int client = db.Clients
                               .FirstOrDefault<Client>(s => s.Name == clientNameInput).ClientID;

                outputDevice.WriteLine("Write stocks amount:");
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


        public void CreateRandomShare()
        {
            using (var db = new TradingContext())
            {
                int maxAmountOfShares = 16;
                int numberOfClients = db.Clients.Count();
                int numberOfStocks = db.Stocks.Count();
                int ClientId = random.Next(1, numberOfClients);
                int StockId = random.Next(1, numberOfStocks);
                int Amount = random.Next(1, maxAmountOfShares);
                AddShare(ClientId, StockId, Amount);
            }
        }
        public void ShowAllShares()
        {
            using (var db = new TradingContext())
            {
                IQueryable<BlockOfShares> query = db.BlockOfSharesTable.OrderBy(b => b.ClientID).AsQueryable<BlockOfShares>();
                tableDrawer.Show(query);
            }
        }
    }
}
