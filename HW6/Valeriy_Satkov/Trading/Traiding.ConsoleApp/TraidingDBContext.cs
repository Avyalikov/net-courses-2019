using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traiding.Core.Models;

namespace Traiding.ConsoleApp
{
    public class TraidingDBContext : DbContext
    {
        public TraidingDBContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        /// <summary>
        /// Represent a set of entities stored in a database
        /// </summary>
        public virtual DbSet<ClientEntity> Clients { get; set; }

        /// <summary>
        /// Represent a set of entities stored in a database
        /// </summary>
        public virtual DbSet<BalanceEntity> Balances { get; set; }

        /// <summary>
        /// Represent a set of entities stored in a database
        /// </summary>
        public virtual DbSet<SharesNumberEntity> SharesNumbers { get; set; }

        /// <summary>
        /// Represent a set of entities stored in a database
        /// </summary>
        public virtual DbSet<ShareTypeEntity> ShareTypes { get; set; }

        /// <summary>
        /// Represent a set of entities stored in a database
        /// </summary>
        public virtual DbSet<ShareEntity> Shares { get; set; } 

        /// <summary>
        /// Represent a set of entities stored in a database
        /// </summary>
        public virtual DbSet<OperationEntity> Operations { get; set; }

        /// <summary>
        /// Represent a set of entities stored in a database
        /// </summary>
        public virtual DbSet<BlockedMoneyEntity> BlockedMoneys { get; set; }

        /// <summary>
        /// Represent a set of entities stored in a database
        /// </summary>
        public virtual DbSet<BlockedSharesNumberEntity> BlockedSharesNumbers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder); // default

            modelBuilder.
                Entity<ClientEntity>().
                HasKey(c => c.Id).
                ToTable("Clients");

            modelBuilder.
                Entity<BalanceEntity>().
                HasKey(b => b.Id).
                ToTable("Balances");

            modelBuilder.
                Entity<SharesNumberEntity>().
                HasKey(n => n.Id).
                ToTable("SharesNumbers");

            modelBuilder.
                Entity<ShareTypeEntity>().
                HasKey(t => t.Id).
                ToTable("ShareTypes");

            modelBuilder.
                Entity<OperationEntity>().
                HasKey(o => o.Id).
                ToTable("Operations");

            modelBuilder.
                Entity<BlockedMoneyEntity>().
                HasKey(m => m.Id).
                ToTable("BlockedMoneys");

            modelBuilder.
                Entity<BlockedSharesNumberEntity>().
                HasKey(n => n.Id).
                ToTable("BlockedSharesNumbers");
        }
    }
}
