using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Trading.Data;

namespace Trading.Interfaces
{
    interface IApplicationContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Share> Shares { get; set; }
        DbSet<TransactionHistory> TransactionHistories { get; set; }
        void AddInitialData();
        int SaveChanges();
    }
}
