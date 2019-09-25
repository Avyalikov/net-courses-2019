namespace SiteParser.Core.Services
{
    using System.Collections.Generic;
    using SiteParser.Core.Models;
    using SiteParser.Core.Repositories;

    public class SaveIntoDatabaseService
    {
        private readonly ISaver saver;

        public SaveIntoDatabaseService(ISaver saver)
        {
            this.saver = saver;
        }

        /// <summary>
        /// Saves Url into Datavbase.
        /// </summary>
        /// <param name="entityToAdd">Link entity.</param>
        /// <returns></returns>
        public string SaveUrl(LinkEntity entityToAdd)
        {
            if (this.saver.Contains(entityToAdd.Link))
            {
                return "This link already exeists in DataBase.";
            }

            var result = this.saver.Save(entityToAdd);
            this.saver.SaveChanges();
            return result;           
        }

        /// <summary>
        /// Saves list of urls into Database.
        /// </summary>
        /// <param name="listOfUrls">List of urls.</param>
        /// <param name="iterationId">Iteration number</param>
        public void SaveUrls(List<string> listOfUrls, int iterationId)
        {
            var listUrlsCopy = new List<string>(listOfUrls);
            LinkEntity linkToAdd = new LinkEntity();
            foreach (string url in listUrlsCopy)
            {
                linkToAdd.Link = url;
                linkToAdd.IterationID = iterationId;
                this.SaveUrl(linkToAdd);
            }
        }
    }
}
