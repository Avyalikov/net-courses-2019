using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading
{
    class TradeValidator : IValidator
    {
        public bool ValidateClient(string[] clientInfo)
        {
            if (clientInfo.Length < 5)
            {
                Logger.MainLog.Warn("Not enough information about client");
                return false;
            }
            decimal balance = 0;
            validateDecimal(out balance, clientInfo[4]);
            return true;
        }

        public bool ValidateShare(string[] shareInfo)
        {
            if (shareInfo.Length < 3)
            {
                Logger.MainLog.Warn("Not enough information about share");
                return false;
            }
            decimal cost = 0;
            validateDecimal(out cost, shareInfo[2]);
            return true;
        }

        public bool ValidateClientMoney(string[] clientInfo)
        {
            if (clientInfo.Length < 3)
            {
                Logger.MainLog.Warn("Not enough information about client balance");
                return false;
            }
            int clientID = 0;
            decimal money = 0;
            validateInt(out clientID, clientInfo[1]);
            validateDecimal(out money, clientInfo[2]);
            return true;
        }

        public bool ValidateShareToClient(TradingDBContext db, string[] shareToClientInfo)
        {
            if (shareToClientInfo.Length < 4)
            {
                Logger.MainLog.Warn("Not enough information about share to client");
                return false;
            }

            int clientID = 0;
            validateInt(out clientID, shareToClientInfo[1]);

            int shareID = 0;
            validateInt(out shareID, shareToClientInfo[2]);

            int amount = 0;
            validateInt(out amount, shareToClientInfo[3]);

            if (db.Clients.Where(x=>x.ClientID==clientID).Count()==0)
            {
                Logger.MainLog.Warn($"Client {clientID} is no exists");
                return false;
            }

            if (db.Shares.Where(x => x.ShareID == shareID).Count() == 0)
            {
                Logger.MainLog.Warn($"Share {shareID} is no exists");
                return false;
            }

            var shareClient = db.ClientsShares.Where(x => x.ClientID == clientID && x.ShareID == shareID).FirstOrDefault();

            if (shareClient != null)
            {
                if (shareClient.Amount + amount <0)
                {
                    Logger.MainLog.Warn($"Shares amount cannot be negative");
                    return false;
                }
            }
            return true;
        }

        public bool ValidateClientList(DbSet<Clients> clients)
        {
            if (clients.Count() < 2)
            {
                Logger.TradeLog.Warn($"Not enough clients to trade");
                return false;
            }
            return true;
        }

        public bool ValidateTradingClient(Clients client)
        {
            if (client.ClientsShares.Count() < 1)
            {
                Logger.TradeLog.Warn($"{client.ClientID} has no shares");
                return false;
            }
            if (client.ClientsShares.Where(x=>x.Amount>0).Count()<1)
            {
                Logger.TradeLog.Warn($"{client.ClientID} has no shares");
                return false;
            }
            return true;
        }

        private bool validateInt(out int result, string checkingString)
        {
            if (!int.TryParse(checkingString, out result))
            {
                Logger.MainLog.Warn($"{checkingString} is not integer");
                return false;
            }
            return true;
        }

        private bool validateDecimal(out decimal result, string checkingString)
        {
            if (!decimal.TryParse(checkingString, out result))
            {
                Logger.MainLog.Warn($"{checkingString} is not decimal");
                return false;
            }
            return true;
        }
    }
}
