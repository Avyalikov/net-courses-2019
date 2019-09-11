namespace HW8_EF
{
    using HW8_EF.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class LinksContext : DbContext
    {
        // Your context has been configured to use a 'LinksContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'HW8_EF.LinksContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'LinksContext' 
        // connection string in the application configuration file.
        public LinksContext()
            : base("name=LinksContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Links> Links { get; set; }
    }
}