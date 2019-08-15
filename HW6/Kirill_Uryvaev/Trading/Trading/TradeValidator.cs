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
                throw new ArgumentException("Not enougth information about client");
            }
            decimal balance = 0;
            if (!decimal.TryParse(clientInfo[4], out balance))
            {
                throw new ArgumentException($"{clientInfo[4]} is not decimal");
            }
            return true;
        }
        public bool ValidateClientList(DbSet<Clients> clients)
        {
            bool isCorrect = true;
            if (clients.Count() < 2)
                isCorrect = false;
            return isCorrect;
        }

        public bool ValidateTradingClient(Clients client)
        {
            bool isCorrect = true;
            if (client.ClientsShares.Count() < 1)
                isCorrect = false;
            return isCorrect;
        }
    }
}
