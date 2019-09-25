using HW7.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW7.Core
{
    public interface IContextProvider
    {
        DbSet<Trader> Traders { get; set; }
        DbSet<Share> Shares { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<Portfolio> Portfolios { get; set; }

        DbEntityEntry Entry(object entity);
        int SaveChanges();
        void Dispose();
    }
}
