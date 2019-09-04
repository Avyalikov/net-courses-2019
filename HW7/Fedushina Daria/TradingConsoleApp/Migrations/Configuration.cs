namespace TradingConsoleApp.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TradingApp.Core.EntityInitializers;
    using TradingApp.Core.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TradingConsoleApp.TradingAppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
        protected override void Seed(TradingAppDbContext db)
        {
        }
    }
}
