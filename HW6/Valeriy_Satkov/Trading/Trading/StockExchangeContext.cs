namespace Trading
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using Entities;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class StockExchangeContext : DbContext
    {
        /// <summary>
        /// Constructs a new context instance(class for interaction with DB) using the given connection string
        /// </summary>
        public StockExchangeContext()
            : base(nameOrConnectionString:"DbConnection")
        {
            Database.SetInitializer(new StockExchangeInitializer());
        }

        /// <summary>
        /// Represent a set of entities stored in a database
        /// </summary>
        public virtual DbSet<Client> Clients { get; set; }

        /// <summary>
        /// Represent a set of entities stored in a database
        /// </summary>
        public virtual DbSet<Share> Shares { get; set; }

        /// <summary>
        /// Represent a set of entities stored in a database
        /// </summary>
        public virtual DbSet<ShareType> ShareTypes { get; set; }

        /// <summary>
        /// Represent a set of entities stored in a database
        /// </summary>
        public virtual DbSet<ClientSharesNumber> ClientSharesNumbers { get; set; }

        /// <summary>
        /// Represent a set of entities stored in a database
        /// </summary>
        public virtual DbSet<Operation> Operations { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Client>()
        //        ;

        //    modelBuilder.Entity<Share>()
        //        ;

        //    modelBuilder.Entity<ShareType>()
        //        ;

        //    modelBuilder.Entity<ClientSharesNumber>()
        //        ;

        //    modelBuilder.Entity<Operation>()
        //        ;
        //}
    }
}
