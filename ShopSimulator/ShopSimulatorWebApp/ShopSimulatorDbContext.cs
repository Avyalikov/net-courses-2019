using ShopSimulator.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSimulator.ConsoleApp
{
    public class ShopSimulatorDbContext : DbContext
    {
        public DbSet<SupplierEntity> Suppliers { get; set; }

        public DbSet<GoodsTableEntity> Goods { get; set; }

        public DbSet<SoldGoodsTableEntity> SoldGoods { get; set; }

        public DbSet<SaleHistoryTableEntity> SaleHistory { get; set; }

        public ShopSimulatorDbContext(string connectionString): base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                 .Entity<SupplierEntity>()
                 .HasKey(p => p.Id)
                 .ToTable("Suppliers");

            modelBuilder
                .Entity<GoodsTableEntity>()
                .HasKey(p => p.Id)
                .ToTable("Goods");

            modelBuilder
                .Entity<SoldGoodsTableEntity>()
                .HasKey(p => p.Id)
                .ToTable("SoldGoods");

            modelBuilder
              .Entity<SaleHistoryTableEntity>()
              .HasKey(p => p.Id)
              .ToTable("SaleHistory");
        }
    }
}
