namespace Trading
{
    using System;
    using System.Collections.Generic;
    using Entities;
    using System.Linq;
    using System.Collections;
    using System.Text;
    using System.Threading.Tasks;

    class StockExchangeInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<StockExchangeContext>
    {
        protected override void Seed(StockExchangeContext context)
        {
            // List of clients
            var clientList = new List<Client>()
            {
                new Client() { LastName = "ServiceAccount1", FirstName = "NameServiceAccount1", Phone = "+0(000)0000000", Balance = 100000},
                new Client() { LastName = "ServiceAccount2", FirstName = "NameServiceAccount2", Phone = "+0(000)0000001", Balance = 100000},
                new Client() { LastName = "Pavlov", FirstName = "Ivan", Phone = "+7(812)5551243", Balance = 38000},
                new Client() { LastName = "Mechnikov", FirstName = "Ilya", Phone = "+33(0)140205317", Balance = 42000},
                new Client() { LastName = "Bunin", FirstName = "Ivan", Phone = "+33(0)420205320", Balance = 30000},
                new Client() { LastName = "Semyonov", FirstName = "Nikolay", Phone = "+7(495)4652317", Balance = 48000},
                new Client() { LastName = "Pasternak", FirstName = "Boris", Phone = "+7(495)4368173", Balance = 35500},
                new Client() { LastName = "Cherenkov", FirstName = "Pavel", Phone = "+7(495)3246421", Balance = 39700},
                new Client() { LastName = "Tamm", FirstName = "Igor", Phone = "+7(495)7523146", Balance = 39700},
                new Client() { LastName = "Frank", FirstName = "Ilya", Phone = "+7(495)7924194", Balance = 31000},
                new Client() { LastName = "Landau", FirstName = "Lev", Phone = "+7(495)7924194", Balance = 55000}                
            };
            clientList.ForEach(c => context.Clients.Add(c));
            context.SaveChanges();

            // Share types
            var shareTypesList = new List<ShareType>()
            {
                new ShareType() { Name = "Cheap", Cost=1000},
                new ShareType() { Name = "Middle", Cost=4000},
                new ShareType() { Name = "Expensive", Cost=10000}
            };
            shareTypesList.ForEach(shT => context.ShareTypes.Add(shT));
            context.SaveChanges();

            // Shares (Name and other info)
            var sharesList = new List<Share>()
            {
                new Share() { CompanyName = "Service", ShareType = shareTypesList[1]/*Middle*/}
            };
            sharesList.ForEach(sh => context.Shares.Add(sh));
            context.SaveChanges();

            // Quantity each share for each client
            var clientSharesNumbers = new List<ClientSharesNumber>()
            {
                new ClientSharesNumber()
                {
                    Client = clientList[0],
                    Share = sharesList[0],
                    Number = 17
                }
            };
            clientSharesNumbers.ForEach(cSN => context.ClientSharesNumbers.Add(cSN));
            context.SaveChanges();

            // List of operations (statistic)
            var operations = new List<Operation>()
            {
                new Operation()
                {
                    DebitDate = DateTime.Now,
                    Customer = clientList[1], // 'ServiceAccount2'
                    ChargeDate = DateTime.Now,
                    Seller = clientList[0], // 'ServiceAccount1'
                    Share = sharesList[0], // 'Service' share
                    Type = sharesList[0].ShareType, // 'Middle'
                    Cost = sharesList[0].ShareType.Cost, // 'Middle' cost
                    Number = 12,
                    Total = 12 * context.ShareTypes.Where(shT => shT.Name == "Middle").FirstOrDefault().Cost
                }
            };
            operations.ForEach(op => context.Operations.Add(op));
            context.SaveChanges();

            base.Seed(context); // default call for override
        }
    }
}
