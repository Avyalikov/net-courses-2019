// <copyright file="DBInit.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace LinkDBContext.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data.Entity;
    using UrlLinksCore.Model;

    /// <summary>
    /// DBInit description
    /// </summary>
    public class DBInit : System.Data.Entity.CreateDatabaseIfNotExists<LinksContext>
    {
        protected override void Seed(LinksContext context)
        {
            base.Seed(context);
            var links = new List<Link>
            {

            };

            context.SaveChanges();

        }
    }
}
