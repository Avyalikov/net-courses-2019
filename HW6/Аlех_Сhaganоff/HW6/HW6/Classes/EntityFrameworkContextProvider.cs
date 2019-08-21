using HW6.DataModel;
using HW6.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW6.Classes
{
    public class EntityFrameworkContextProvider : IContextProvider
    {
        private TradingContext context = new TradingContext();
       
        public virtual DbSet<Trader> Traders { get; set; }
        public virtual DbSet<Share> Shares { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Portfolio> Portfolios { get; set; }
        
        public EntityFrameworkContextProvider()
        {
            Traders = context.Traders;
            Shares = context.Shares;
            Transactions = context.Transactions;
            Portfolios = context.Portfolios;
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public virtual void Dispose()
        {
            context.Dispose();
        }
    }
}
