using HW6.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW6.Interfaces
{
    public interface IContextProvider
    {
        DbSet<Trader> Traders { get; set; }
        DbSet<Share> Shares { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<Portfolio> Portfolios { get; set; }

        int SaveChanges();
        void Dispose();
    }
}
