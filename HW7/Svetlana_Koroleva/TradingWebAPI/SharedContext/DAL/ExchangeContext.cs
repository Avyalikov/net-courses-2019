// <copyright file="ExchangeContext.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace SharedContext.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data.Entity;
    using Trading.Core.Model;
    using Microsoft.EntityFrameworkCore;
    

    /// <summary>
    /// ExchangeContext description
    /// </summary>
    public class ExchangeContext: DbContext
    {

        public ExchangeContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {

          //  Database.EnsureCreated();
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientStock> ClientStocks { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);           
            modelBuilder.Entity<ClientStock>().HasKey(k => new { k.ClientID, k.StockID });

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=StockExchangeW;Trusted_Connection=True;");
        }
    }
}
