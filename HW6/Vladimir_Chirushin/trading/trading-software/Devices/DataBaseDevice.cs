using System.Collections.Generic;
using System.Linq;

namespace trading_software
{
    public class DataBaseDevice : IDataBaseDevice
    {
        public decimal GetStockPrice(int stockID)
        {
            using (var db = new TradingContext())
            {
                return db.Stocks.Where(c => c.StockID == stockID).FirstOrDefault().Price;
            }
        }

        public IEnumerable<Client> GetAllClients()
        {
            using (var db = new TradingContext())
            {
                return db.Clients.OrderBy(c => c.Name).AsEnumerable<Client>().ToList();
            }
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            using (var db = new TradingContext())
            {
                return db.Stocks.OrderBy(s=>s.StockType).AsEnumerable<Stock>().ToList();
            }
        }
        public IEnumerable<BlockOfShares> GetAllBlockOfShares()
        {
            using (var db = new TradingContext())
            {
                return db.BlockOfSharesTable.OrderBy(b => b.ClientID).AsEnumerable<BlockOfShares>().ToList();
            }
        }

        public IEnumerable<Transaction> GetAllTransaction()
        {
            using (var db = new TradingContext())
            {
                IEnumerable<Transaction> query = db.TransactionHistory.AsEnumerable<Transaction>().ToList();
                return query;
            }
        }

        public int GetNumberOfClients()
        {
            using (var db = new TradingContext())
            {
                return db.Clients.Count();
            }
        }

        public int GetNumberOfStocks()
        {
            using (var db = new TradingContext())
            {
                return db.Stocks.Count();
            }
        }

        void IDataBaseDevice.Add(BlockOfShares blockOfShares)
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

        bool IDataBaseDevice.Add(Client client)
        {
            using (var db = new TradingContext())
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return true;
            }
        }

        bool IDataBaseDevice.Add(Stock stock)
        {
            using (var db = new TradingContext())
            {
                db.Stocks.Add(stock);
                db.SaveChanges();
                return true;
            }
        }

        bool IDataBaseDevice.Add(Transaction transaction)
        {
            using (var db = new TradingContext())
            {
                db.TransactionHistory.Add(transaction);
                db.SaveChanges();
                return true;
            }
        }

        string IDataBaseDevice.GetClientName(int ClientID)
        {
            using (var db = new TradingContext())
            {
                return db.Clients.Where(c => c.ClientID == ClientID).FirstOrDefault().Name;
            }
        }

        string IDataBaseDevice.GetStockType(int StockID)
        {
            using (var db = new TradingContext())
            {
                return db.Stocks.Where(s => s.StockID == StockID).FirstOrDefault().StockType;
            }
        }

        public IEnumerable<Client> GetBlackClients()
        {
            using (var db = new TradingContext())
            {
                IEnumerable<Client> query = db.Clients.Where(c => c.Balance < 0)
                    .OrderBy(c => c.Name).AsEnumerable<Client>();
                return query;
            }
        }

        public IEnumerable<Client> GetOrangeClients()
        {
            using (var db = new TradingContext())
            {
                IEnumerable<Client> query = db.Clients.Where(c => c.Balance == 0)
                    .OrderBy(c => c.Name).AsEnumerable<Client>();
                return query;
            }
        }

        public bool IsClientExist(int ClientID)
        {
            using (var db = new TradingContext())
            {
                return db.Clients.Where(c => c.ClientID == ClientID).FirstOrDefault() != null;
            }
        }
        public bool IsClientExist(string ClientName)
        {
            using (var db = new TradingContext())
            {
                return db.Clients.Where(c => c.Name == ClientName).FirstOrDefault() != null;
            }
        }
        public bool ChangeBalance(int ClientID, decimal accountGain)
        {
            using (var db = new TradingContext())
            {
                Client client = db.Clients.Where(c => c.ClientID == ClientID).FirstOrDefault();
                client.Balance += accountGain;
                db.SaveChanges();
                return true;
            }
        }

        public void BankruptClient(int ClientID)
        {
            using (var db = new TradingContext())
            {
                Client client = db.Clients.Where(c => c.ClientID == ClientID).FirstOrDefault();
                client.Balance = 0;
                db.SaveChanges();
            }
        }

        public bool IsStockExist(int StockID)
        {
            using (var db = new TradingContext())
            {
                return db.Stocks.Where(s => s.StockID == StockID).FirstOrDefault() != null;
            }
        }

        public bool IsStockExist(string StockType)
        {
            using (var db = new TradingContext())
            {
                return db.Stocks.Where(c => c.StockType == StockType).FirstOrDefault() != null;
            }
        }

        public int GetClientID(string ClientName)
        {
            using (var db = new TradingContext())
            {
                return db.Clients.Where(c => c.Name == ClientName).FirstOrDefault().ClientID;
            }
        }

        public decimal GetClientBalance(int ClientID)
        {
            using (var db = new TradingContext())
            {
                return db.Clients.Where(c => c.ClientID == ClientID).FirstOrDefault().Balance;
            }
        }

        public int GetStockID(string StockType)
        {
            using (var db = new TradingContext())
            {
                return db.Stocks.Where(c => c.StockType == StockType).FirstOrDefault().StockID;
            }
        }

        public int GetClientStockAmount(int ClientID, int StockID)
        {
            using (var db = new TradingContext())
            {
                return db.BlockOfSharesTable.Where(b => b.StockID == StockID && b.ClientID == ClientID).FirstOrDefault().Amount;
            }
        }

        public bool IsClientHasStockType(int ClientID, int StockID)
        {
            using (var db = new TradingContext())
            {
                return db.BlockOfSharesTable.Where(b => b.StockID == StockID && b.ClientID == ClientID).FirstOrDefault() != null;
            }
        }
    }
}
