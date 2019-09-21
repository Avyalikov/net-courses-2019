namespace stockSimulator.Modulation
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using stockSimulator.Core.Models;

    internal class StockInitializer : DropCreateDatabaseAlways<StockSimulatorDbContext>
    {
        /// <summary>
        /// Executes the strategy to initialize the database for the given context.
        /// </summary>
        /// <param name="context">Derived from DbContext instance.</param>
        public override void InitializeDatabase(StockSimulatorDbContext context)
        {
            context.Database.ExecuteSqlCommand(
                TransactionalBehavior.DoNotEnsureTransaction, 
                string.Format(
                    "ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", 
                    context.Database.Connection.Database));

            base.InitializeDatabase(context);
        }

        /// <summary>
        /// Fills DataBase by initial data.
        /// </summary>
        /// <param name="context">Derived from DbContext instance.</param>
        protected override void Seed(StockSimulatorDbContext context)
        {
            var clients = new List<ClientEntity>
            {
                new ClientEntity { Name = "Danny", Surname = "Libovsky", CreateAt = DateTime.Parse("2005-09-01"), PhoneNumber = "+79654161414", AccountBalance = 1000 },
                new ClientEntity { Name = "Larisa", Surname = "Dolina", CreateAt = DateTime.Parse("2005-12-01"), PhoneNumber = "+76321458552", AccountBalance = 5000 },
                new ClientEntity { Name = "Patrick", Surname = "Star", CreateAt = DateTime.Parse("2006-05-01"), PhoneNumber = "+79654513154", AccountBalance = 2500 },
                new ClientEntity { Name = "John", Surname = "Lenon", CreateAt = DateTime.Parse("2000-03-01"), PhoneNumber = "+34516485161", AccountBalance = 9000 },
                new ClientEntity { Name = "Mike", Surname = "Vazovsky", CreateAt = DateTime.Parse("2003-07-01"), PhoneNumber = "+7654165165", AccountBalance = 4000 },
            };

            clients.ForEach(c => context.Clients.Add(c));
            context.SaveChanges();

            var stocks = new List<StockEntity>
            {
                new StockEntity { Name = "Yandex", Type = "P", Cost = 500, },
                new StockEntity { Name = "Microsoft", Type = "O", Cost = 300, },
                new StockEntity { Name = "Nokia", Type = "P", Cost = 400, },
                new StockEntity { Name = "Sony", Type = "O", Cost = 250, },
                new StockEntity { Name = "Electronic Arts", Type = "P", Cost = 370, },
                new StockEntity { Name = "Gazprom", Type = "0", Cost = 280, },
                new StockEntity { Name = "Lukoil", Type = "P", Cost = 200, },
                new StockEntity { Name = "Nike", Type = "0", Cost = 190, },
            };

            stocks.ForEach(s => context.Stocks.Add(s));
            context.SaveChanges();

            var stockOfClients = new List<StockOfClientsEntity>
            {
                new StockOfClientsEntity { ClientID = 1, StockID = 3, Amount = 5 },
                new StockOfClientsEntity { ClientID = 2, StockID = 2, Amount = 5 },
                new StockOfClientsEntity { ClientID = 3, StockID = 1, Amount = 5 },
                new StockOfClientsEntity { ClientID = 5, StockID = 4, Amount = 5 },
                new StockOfClientsEntity { ClientID = 5, StockID = 5, Amount = 5 },
                new StockOfClientsEntity { ClientID = 3, StockID = 6, Amount = 5 },
                new StockOfClientsEntity { ClientID = 2, StockID = 7, Amount = 5 },
                new StockOfClientsEntity { ClientID = 1, StockID = 8, Amount = 5 },
                new StockOfClientsEntity { ClientID = 2, StockID = 8, Amount = 5 },
                new StockOfClientsEntity { ClientID = 3, StockID = 7, Amount = 5 },
            };

            stockOfClients.ForEach(sc => context.StockOfClients.Add(sc));
            context.SaveChanges();
        }
    }
}
