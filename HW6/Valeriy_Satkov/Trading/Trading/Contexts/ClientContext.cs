namespace Trading.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using Trading.Persons;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class ClientContext : DbContext
    {
        /// <summary>
        /// Constructs a new context instance using the given connection string
        /// </summary>
        public ClientContext() 
            : base("DbConnection")
        { }

        /// <summary>
        /// Represent a set of entities stored in a database
        /// </summary>
        public DbSet<Client> Clients { get; set; }
    }
}
