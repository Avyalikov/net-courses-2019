namespace SiteParser.Simulator
{
    using System.Data.Entity;
    using SiteParser.Core.Models;

    /// <summary>
    /// Connect to DB.
    /// </summary>
    internal class SiteParserDbContext : DbContext
    {
        /// <summary>
        /// Table of links.
        /// </summary>
        public DbSet<LinkEntity> Links { get; set; }

        public SiteParserDbContext() : base("name=stockSimulatorConnectionString")
        {
            Database.SetInitializer(new DbInitializer());
        }
    }
}