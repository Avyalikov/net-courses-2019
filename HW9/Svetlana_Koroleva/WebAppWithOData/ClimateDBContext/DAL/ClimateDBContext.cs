// <copyright file="ClimateDBContext.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace ClimateDBContext.DAL
{
    using ClimateDBContext.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// ClimateDBContext description
    /// </summary>
    public class ClimateDbContext:DbContext
    {
       public  ClimateDbContext() : base("Climates")
        {
            Database.SetInitializer<ClimateDbContext>(new DBInitializer());
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CityClimateInfo> ClimateInfos { get; set; }
    }
}
