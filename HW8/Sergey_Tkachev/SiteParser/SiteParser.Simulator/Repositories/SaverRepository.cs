namespace SiteParser.Simulator.Repositories
{
    using System;
    using System.Linq;
    using SiteParser.Core.Models;
    using SiteParser.Core.Repositories;

    internal class SaverRepository : ISaver
    {
        private readonly SiteParserDbContext dbContext;

        public SaverRepository(SiteParserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Checks if in DataBase already exists an Url.
        /// </summary>
        /// <param name="urlToCheck">Url to check in DataBase.</param>
        /// <returns></returns>
        public bool Contains(string urlToCheck)
        {
            return this.dbContext.Links
               .Any(l => l.Link == urlToCheck);
        }

        /// <summary>
        /// Adds a new entr into DataBase.
        /// </summary>
        /// <param name="entityToAdd">Entry to add</param>
        /// <returns></returns>
        public string Save(LinkEntity entityToAdd)
        {
            string result = string.Empty;
            try
            {
                this.dbContext.Links.Add(entityToAdd);
            }
            catch (Exception ex)
            {
                result = "Error by entery inserting into Database. " + ex.Message;
                return result;
            }

            result = "Entity was successfully inserted into Database.";
            return result;
        }

        /// <summary>
        /// Saves changes in DataBase.
        /// </summary>
        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }
}
