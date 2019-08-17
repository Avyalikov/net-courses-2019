using TradingApp.DAL;
using TradingApp.Model;
using TradingApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApp
{
    public class ClientStockModifier:IClientStocksModifier
    {
        private ExchangeContext db;
        public ClientStockModifier(ExchangeContext db)
        {
            this.db = db;
        }
        public bool CheckClientStock(int clientId, int stockId)
        {
            bool isExists = false;
            var clientstock = db.ClientStocks.Where(c => c.ClientID == clientId && c.StockID == stockId).FirstOrDefault();
            if (clientstock != null)
            {
                isExists = true;
            }
            return isExists;
        }

        public void AddClientStock(int clientId, int stockId)
            {
                ClientStock clientStock = new ClientStock { ClientID = clientId, StockID = stockId, Quantity = 0 };
                db.ClientStocks.Add(clientStock);
                db.SaveChanges();

            }
        
        public void EditClientStocks(Order custOrder, Order salerOrder)
        {

            db.ClientStocks.Where(c => c.ClientID == salerOrder.ClientID && c.StockID == salerOrder.StockID).Single().Quantity -= salerOrder.Quantity;
            bool clientStockIsExists=CheckClientStock(custOrder.ClientID, custOrder.StockID);
            if (clientStockIsExists == false)
            {
                AddClientStock(custOrder.ClientID, custOrder.StockID);
            }
            db.ClientStocks.Where(c => c.ClientID == custOrder.ClientID && c.StockID == custOrder.StockID).Single().Quantity += custOrder.Quantity;
            db.SaveChanges();

        }
    }
}
