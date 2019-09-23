namespace SiteParser.Simulator
{
    using System.Data.Entity;

    internal class DbInitializer : DropCreateDatabaseAlways<SiteParserDbContext>
    {
        /// <summary>
        /// Deletes the DataBase before initializing.
        /// </summary>
        /// <param name="context">DataBase context.</param>
        public override void InitializeDatabase(SiteParserDbContext context)
        {
            context.Database.ExecuteSqlCommand(
                TransactionalBehavior.DoNotEnsureTransaction,
                string.Format("ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));

            base.InitializeDatabase(context);
        }

        /// <summary>
        /// Fills the DataBase by its creation.
        /// </summary>
        /// <param name="context">DataBase context.</param>
        protected override void Seed(SiteParserDbContext context)
        {
        }
    }
}
