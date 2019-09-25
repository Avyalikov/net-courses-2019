namespace HW6
{
    using HW6.DataModel;
    using HW6.Interfaces;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class TradingContext : DbContext, IContextProvider
    {
        // Your context has been configured to use a 'TradingContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'TradingDb' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'TradingContext' 
        // connection string in the application configuration file.
        public TradingContext()
            : base("name=TradingContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Trader> Traders { get; set; }
        public virtual DbSet<Share> Shares { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trader>()
            .Map(map => { map.Properties(p => new { p.TraderId, p.FirstName, p.LastName, p.PhoneNumber }); map.ToTable("Traders"); })
            .Map(map => { map.Properties(p => new { p.TraderId, p.Balance }); map.ToTable("TraderBalance"); });

            modelBuilder.Entity<Share>()
            .Map(map => { map.Properties(p => new { p.ShareId, p.Name}); map.ToTable("Shares"); })
            .Map(map => { map.Properties(p => new { p.ShareId, p.Price }); map.ToTable("SharePrice"); });
        }
    }
}