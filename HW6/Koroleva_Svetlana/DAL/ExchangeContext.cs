// <copyright file="ExchangeContext.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace ConsoleApp16.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ConsoleApp16.Model;
     using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Data.Entity;

    /// <summary>
    /// ExchangeContext description
    /// </summary>
    public class ExchangeContext: DbContext
    {

        static ExchangeContext()
        {
            Database.SetInitializer<ExchangeContext>(new StockExchangeInitializer());
        }

        public ExchangeContext():base("ExchangeContext")
        { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientStock> ClientStocks { get; set; }
        public DbSet<Issuer> Issuers { get; set; }
        public DbSet<PriceHistory> PriceHistories { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<TransactionHistory> transactionHistories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ClientStock>().HasKey(k => new { k.ClientID, k.StockID });

        }
    }
}
