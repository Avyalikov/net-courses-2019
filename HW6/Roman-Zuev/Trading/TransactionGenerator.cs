using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.DataModel
{
    class TransactionGenerator: ITransactionGenerator
    {
        Settings settings;

        public TransactionGenerator (Settings settings)
        {
            this.settings = settings;
        }

        public void GenerateTransactionParams(out int sellerId, out int buyerId, out int sharesId, out int quantity)
        {
            using (TradingDBContext db = new TradingDBContext())
            {
                try
                {
                    Random random = new Random();
                    sellerId = db.Clients.ToList()[random.Next(0, 100) % db.Clients.ToList().Count].ClientID;
                    do
                    {
                        buyerId = db.Clients.ToList()[random.Next(0, 100) % db.Clients.ToList().Count].ClientID;
                    } while (sellerId == buyerId);

                    sharesId = db.Shares.ToList()[random.Next(0, 100) % db.Shares.ToList().Count].SharesID;
                    quantity = random.Next(1, settings.MaxSharesToSell);
                    return;
                }
                catch (SystemException ex)
                {
                    throw ex;
                }
            }
        }
    }
}
