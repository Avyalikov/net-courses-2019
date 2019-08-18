namespace Trading.DataModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class TradingDBContext : DbContext
    {
        public TradingDBContext()
            : base("name=TradingDBContext")
        {
            Database.SetInitializer(new TradingDBInitializer());
        }
        public virtual DbSet<Client> Clients { get; set; }
        //public virtual DbSet<ClientFund> ClientFunds { get; set; }
        public virtual DbSet<ClientShares> ClientShares { get; set; }
        public virtual DbSet<Shares> Shares { get; set; }
        public virtual DbSet<TransactionHistory> TransactionHistories { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{

        //    modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        //    modelBuilder.Entity<TransactionHistory>()
        //       .HasRequired(f => f.Buyer)
        //       .WithRequiredDependent()
        //       .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<TransactionHistory>()
        //       .HasRequired(f => f.Seller)
        //       .WithRequiredDependent()
        //       .WillCascadeOnDelete(false);
        //}
    }
}