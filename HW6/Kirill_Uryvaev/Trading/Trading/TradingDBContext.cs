namespace Trading
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TradingDBContext : DbContext
    {
        public TradingDBContext()
            : base("name=TradingDBContext")
        {
        }

        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<ClientsShares> ClientsShares { get; set; }
        public virtual DbSet<Shares> Shares { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clients>()
                .Property(e => e.ClientFirstName)
                .IsFixedLength();

            modelBuilder.Entity<Clients>()
                .Property(e => e.ClientLastName)
                .IsFixedLength();

            modelBuilder.Entity<Clients>()
                .Property(e => e.PhoneNumber)
                .IsFixedLength();

            modelBuilder.Entity<Clients>()
                .Property(e => e.ClientBalance)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Clients>()
                .HasMany(e => e.ClientsShares)
                .WithRequired(e => e.Clients)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shares>()
                .Property(e => e.ShareName)
                .IsFixedLength();

            modelBuilder.Entity<Shares>()
                .Property(e => e.ShareCost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Shares>()
                .HasMany(e => e.ClientsShares)
                .WithRequired(e => e.Shares)
                .WillCascadeOnDelete(false);
        }
    }
}
