using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Trading
{
    interface IValidator
    {
        bool ValidateClient(string[] clientInfo);
        bool ValidateShare(string[] shareInfo);
        bool ValidateClientMoney(string[] clientInfo);
        bool ValidateShareToClient(TradingDBContext db, string[] shareToClientInfo);
        bool ValidateClientList(DbSet<Clients> clients);
        bool ValidateTradingClient(Clients client);
    }
}
