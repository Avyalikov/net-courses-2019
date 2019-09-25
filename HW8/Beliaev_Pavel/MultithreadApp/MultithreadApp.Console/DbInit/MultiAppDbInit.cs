using MultithreadApp.Core.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadApp.Console.DbInit
{
    public class MultiAppDbInit : System.Data.Entity.DropCreateDatabaseIfModelChanges<MultiAppDbContext>
    {
        public override void InitializeDatabase(MultiAppDbContext context)
        {
            SqlConnection.ClearAllPools();
            base.InitializeDatabase(context);
        }

        protected override void Seed(MultiAppDbContext context)
        {
            base.Seed(context);

            var links = new List<Url>
            {};

            context.SaveChanges();
        }
    }
}
