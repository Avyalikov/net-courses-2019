using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Trading
{
    class ClientsOperations : IOperations
    {
        private readonly IValidator validator;

        Timer operationTimer;
        Random uniformRandomiser;
        TradingDBContext db;

        public ClientsOperations(IValidator validator)
        {
            this.validator = validator;
            operationTimer = new Timer(10000);
            operationTimer.Elapsed += clientsTrade;
            uniformRandomiser = new Random();
        }

        public void StartTradingOperations(object dbObject)
        {
            db = (TradingDBContext)dbObject;
            operationTimer.AutoReset = true;
            operationTimer.Start();
        }

        private void clientsTrade(object source, ElapsedEventArgs e)
        {
            if (validator.ValidateClientList(db.Clients))
            {
                var tradingClients = db.Clients.OrderBy(x => Guid.NewGuid()).Take(2).ToList();
                if (validator.ValidateTradingClient(tradingClients[0]))
                {
                    ClientsShares shareType = tradingClients[0].ClientsShares.Where(x=>x.Amount>0).OrderBy(x => Guid.NewGuid()).First();
                    int numberOfSoldShares = uniformRandomiser.Next(1, (int)shareType.Amount);
                    decimal shareCost = (decimal)db.Shares.Where(x => x.ShareID == shareType.ShareID).Select(x => x.ShareCost).FirstOrDefault();
                    shareType.Amount -= numberOfSoldShares;
                    tradingClients[1].ClientBalance -= numberOfSoldShares * shareCost;
                    if (tradingClients[1].ClientsShares.Where(x=>x.ShareID==shareType.ShareID).Count()<1)
                    {
                        var clientShares = new ClientsShares() { ShareID = shareType.ShareID, ClientID = tradingClients[1].ClientID, Amount = numberOfSoldShares };
                        tradingClients[1].ClientsShares.Add(clientShares);
                    }
                    else
                    {
                        tradingClients[1].ClientsShares.Where(x => x.ShareID == shareType.ShareID).FirstOrDefault().Amount+= numberOfSoldShares;
                    }
                    Logger.TradeLog.Info($"Client {tradingClients[0].ClientID} sold {numberOfSoldShares} shares of {shareType.ShareID} to {tradingClients[1].ClientID}");
                    db.SaveChanges();
                }
            }
        }
    }
}
