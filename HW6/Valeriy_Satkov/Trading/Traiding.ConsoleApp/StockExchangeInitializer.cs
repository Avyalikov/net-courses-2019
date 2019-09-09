namespace Traiding.ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Traiding.Core.Models;

    public class StockExchangeInitializer : DropCreateDatabaseIfModelChanges<StockExchangeDBContext>
    {
        protected override void Seed(StockExchangeDBContext context)
        {
            // List of clients
            var clients = new List<ClientEntity>()
            {
                new ClientEntity() { Id = 1, CreatedAt = DateTime.Now, LastName = "Pavlov", FirstName = "Ivan", PhoneNumber = "+7(812)5551243", Status = true },
                new ClientEntity() { Id = 2, CreatedAt = DateTime.Now, LastName = "Mechnikov", FirstName = "Ilya", PhoneNumber = "+33(0)140205317", Status = true },
                new ClientEntity() { Id = 3, CreatedAt = DateTime.Now, LastName = "Bunin", FirstName = "Ivan", PhoneNumber = "+33(0)420205320", Status = true },
                new ClientEntity() { Id = 4, CreatedAt = DateTime.Now, LastName = "Semyonov", FirstName = "Nikolay", PhoneNumber = "+7(495)4652317", Status = true },
                new ClientEntity() { Id = 5, CreatedAt = DateTime.Now, LastName = "Pasternak", FirstName = "Boris", PhoneNumber = "+7(495)4368173", Status = true },
                new ClientEntity() { Id = 6, CreatedAt = DateTime.Now, LastName = "Cherenkov", FirstName = "Pavel", PhoneNumber = "+7(495)3246421", Status = true },
                new ClientEntity() { Id = 7, CreatedAt = DateTime.Now, LastName = "Tamm", FirstName = "Igor", PhoneNumber = "+7(495)7523146", Status = true },
                new ClientEntity() { Id = 8, CreatedAt = DateTime.Now, LastName = "Frank", FirstName = "Ilya", PhoneNumber = "+7(495)7924194", Status = true },
                new ClientEntity() { Id = 9, CreatedAt = DateTime.Now, LastName = "Landau", FirstName = "Lev", PhoneNumber = "+7(495)7924194", Status = true }
            };
            clients.ForEach(c => context.Clients.Add(c));
            context.SaveChanges();

            var balances = new List<BalanceEntity>()
            {
                new BalanceEntity() { Id = 1, Client = clients[0], Amount = 138000, Status = true },
                new BalanceEntity() { Id = 2, Client = clients[1], Amount = 142000, Status = true },
                new BalanceEntity() { Id = 3, Client = clients[2], Amount = 130000, Status = true },
                new BalanceEntity() { Id = 4, Client = clients[3], Amount = 148000, Status = true },
                new BalanceEntity() { Id = 5, Client = clients[4], Amount = 135500, Status = true },
                new BalanceEntity() { Id = 6, Client = clients[5], Amount = 139700, Status = true },
                new BalanceEntity() { Id = 7, Client = clients[6], Amount = 139700, Status = true },
                new BalanceEntity() { Id = 8, Client = clients[7], Amount = 131000, Status = true },
                new BalanceEntity() { Id = 9, Client = clients[8], Amount = 155000, Status = true },
            };
            balances.ForEach(c => context.Balances.Add(c));
            context.SaveChanges();

            // Share types
            var shareTypesList = new List<ShareTypeEntity>()
            {
                new ShareTypeEntity() { Id = 1, Name = "Cheap", Cost=1000, Status = true},
                new ShareTypeEntity() { Id = 2, Name = "Middle", Cost=4000, Status = true},
                new ShareTypeEntity() { Id = 3, Name = "Expensive", Cost=10000, Status = true}
            };
            shareTypesList.ForEach(shT => context.ShareTypes.Add(shT));
            context.SaveChanges();

            // Shares (Name and other info)
            var sharesList = new List<ShareEntity>()
            {
                new ShareEntity() { Id = 1, CreatedAt = DateTime.Now, CompanyName = "Microsoft", Type = shareTypesList[2]/*Expensive*/, Status = true},
                new ShareEntity() { Id = 2, CreatedAt = DateTime.Now, CompanyName = "Apple", Type = shareTypesList[1]/*Middle*/, Status = true},
                new ShareEntity() { Id = 3, CreatedAt = DateTime.Now, CompanyName = "Yandex", Type = shareTypesList[1]/*Middle*/, Status = true}
            };
            sharesList.ForEach(sh => context.Shares.Add(sh));
            context.SaveChanges();

            // Quantity each share for each client
            var sharesNumbers = new List<SharesNumberEntity>()
            {
                new SharesNumberEntity()
                {
                    Id = 1,
                    Client = clients[0], // Id = 1
                    Share = sharesList[0], // Microsoft
                    Number = 50
                },
                new SharesNumberEntity()
                {
                    Id = 2,
                    Client = clients[1], // Id = 2
                    Share = sharesList[0], // Microsoft
                    Number = 30
                },
                new SharesNumberEntity()
                {
                    Id = 3,
                    Client = clients[1], // Id = 2
                    Share = sharesList[1], // Apple
                    Number = 40
                },
                new SharesNumberEntity()
                {
                    Id = 4,
                    Client = clients[1], // Id = 2
                    Share = sharesList[2], // Yandex
                    Number = 30
                },
                new SharesNumberEntity()
                {
                    Id = 5,
                    Client = clients[0], // Id = 1
                    Share = sharesList[1], // Apple
                    Number = 20
                },
                new SharesNumberEntity()
                {
                    Id = 6,
                    Client = clients[5], // Id = 6
                    Share = sharesList[2], // Yandex
                    Number = 30
                },
                new SharesNumberEntity()
                {
                    Id = 7,
                    Client = clients[4], // Id = 5
                    Share = sharesList[0], // Microsoft
                    Number = 40
                },
                new SharesNumberEntity()
                {
                    Id = 8,
                    Client = clients[3], // Id = 4
                    Share = sharesList[0], // Microsoft
                    Number = 25
                }
            };
            sharesNumbers.ForEach(cSN => context.SharesNumbers.Add(cSN));
            context.SaveChanges();

            // List of operations (statistic)
            var operations = new List<OperationEntity>()
            {
                new OperationEntity()
                {
                    Id = 1,
                    DebitDate = DateTime.Now,
                    Customer = clients[1],
                    ChargeDate = DateTime.Now,
                    Seller = clients[0],
                    Share = sharesList[0], // 'Service' share
                    ShareTypeName = sharesList[0].Type.Name, // 'Middle'
                    Cost = sharesList[0].Type.Cost, // 'Middle' cost
                    Number = 12,
                    Total = 12 * sharesList[0].Type.Cost
                },
                new OperationEntity()
                {
                    Id = 2,
                    DebitDate = DateTime.Now,
                    Customer = clients[5],
                    ChargeDate = DateTime.Now,
                    Seller = clients[2],
                    Share = sharesList[0], // 'Service' share
                    ShareTypeName = sharesList[0].Type.Name, // 'Middle'
                    Cost = sharesList[0].Type.Cost, // 'Middle' cost
                    Number = 12,
                    Total = 12 * sharesList[0].Type.Cost
                },
                new OperationEntity()
                {
                    Id = 3,
                    DebitDate = DateTime.Now,
                    Customer = clients[1],
                    ChargeDate = DateTime.Now,
                    Seller = clients[0],
                    Share = sharesList[1], // 'Service' share
                    ShareTypeName = sharesList[0].Type.Name, // 'Middle'
                    Cost = sharesList[1].Type.Cost, // 'Middle' cost
                    Number = 12,
                    Total = 12 * sharesList[0].Type.Cost
                },
                new OperationEntity()
                {
                    Id = 4,
                    DebitDate = DateTime.Now,
                    Customer = clients[1],
                    ChargeDate = DateTime.Now,
                    Seller = clients[0],
                    Share = sharesList[0], // 'Service' share
                    ShareTypeName = sharesList[0].Type.Name, // 'Middle'
                    Cost = sharesList[0].Type.Cost, // 'Middle' cost
                    Number = 12,
                    Total = 12 * sharesList[0].Type.Cost
                },
                new OperationEntity()
                {
                    Id = 5,
                    DebitDate = DateTime.Now,
                    Customer = clients[1],
                    ChargeDate = DateTime.Now,
                    Seller = clients[0],
                    Share = sharesList[0], // 'Service' share
                    ShareTypeName = sharesList[0].Type.Name, // 'Middle'
                    Cost = sharesList[0].Type.Cost, // 'Middle' cost
                    Number = 12,
                    Total = 12 * sharesList[0].Type.Cost
                }
            };
            operations.ForEach(op => context.Operations.Add(op));
            context.SaveChanges();

            base.Seed(context); // default call for override
        }
    }
}
