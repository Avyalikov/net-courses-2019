namespace Multithread.ConsoleApp
{
    using Multithread.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ConnectedLinksDBContext : DbContext
    {
        public ConnectedLinksDBContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        /// <summary>
        /// Represent a set of entities stored in a database
        /// </summary>
        public virtual DbSet<LinkEntity> Links { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.
                Entity<LinkEntity>().
                HasKey(u => u.Id).
                ToTable("Links");
            
            base.OnModelCreating(modelBuilder); // default
        }
    }
}
