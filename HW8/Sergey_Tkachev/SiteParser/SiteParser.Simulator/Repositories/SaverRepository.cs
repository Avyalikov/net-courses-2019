using SiteParser.Core.Models;
using SiteParser.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteParser.Simulator.Repositories
{
    class SaverRepository : ISaver
    {
        private readonly SiteParserDbContext dbContext;

        public SaverRepository(SiteParserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool Contains(string urlToCheck)
        {
            return this.dbContext.Links
               .Any(l => l.Link == urlToCheck);
        }

        public string Save(LinkEntity entityToAdd)
        {
            string result = String.Empty;
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

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }
}
