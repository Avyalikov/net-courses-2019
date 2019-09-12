using SiteParser.Core.Models;
using System.Data.Entity;

namespace SiteParser.Simulator
{
    internal class SiteParserDbContext : DbContext
    {
        public DbSet<LinkEntity> Links { get; set; }

        public SiteParserDbContext() : base("name=StockSimulatorConnectionString")
        {
            Database.SetInitializer(new dbInitializer());
        }
    }
}