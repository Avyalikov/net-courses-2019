namespace TradingSimulator.Model
{
    using System.Data.Entity;

    public class TradingDbContext : DbContext
    {
        // Your context has been configured to use a 'TradingDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'TradingSimulator.DB.TradingDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'TradingDbContext' 
        // connection string in the application configuration file.

        public DbSet<Broker> Brokers { get; set; }
        public DbSet<PersonalCard> Cards { get; set; }
        public DbSet<Share> Shares { get; set; }

        public TradingDbContext() : base("name=TradingDbContext")
        {
            Database.SetInitializer(new TradingDbInitializer());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}