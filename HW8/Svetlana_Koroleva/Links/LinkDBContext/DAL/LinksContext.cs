// <copyright file="LinksContext.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace LinkDBContext.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UrlLinksCore.Model;

    /// <summary>
    /// LinksContext description
    /// </summary>
    public class LinksContext:DbContext 
    {

        static LinksContext()
        {
            Database.SetInitializer<LinksContext>(new DBInit());
        }

        public LinksContext(): base("LinksContext")
        {
            
        }

        public DbSet<Link> Links { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

        }
    }
}
