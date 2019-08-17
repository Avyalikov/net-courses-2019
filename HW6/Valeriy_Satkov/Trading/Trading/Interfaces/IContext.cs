namespace Trading.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;    
    using Entities;

    interface IContext
    {
        /// <summary>
        /// Represent a set of entities stored in a database
        /// </summary>
        DbSet<Client> Clients { get; set; }

        /// <summary>
        /// Represent a set of entities stored in a database
        /// </summary>
        DbSet<Share> Shares { get; set; }

        /// <summary>
        /// Represent a set of entities stored in a database
        /// </summary>
        DbSet<ShareType> ShareTypes { get; set; }

        /// <summary>
        /// Represent a set of entities stored in a database
        /// </summary>
        DbSet<ClientSharesNumber> ClientSharesNumbers { get; set; }

        /// <summary>
        /// Represent a set of entities stored in a database
        /// </summary>
        DbSet<Operation> Operations { get; set; }

        int SaveChanges();
    }
}
