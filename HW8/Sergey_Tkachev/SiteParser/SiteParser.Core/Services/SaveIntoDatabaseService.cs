using SiteParser.Core.Models;
using SiteParser.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiteParser.Core.Services
{
    public class SaveIntoDatabaseService
    {
        private readonly ISaver saver;
        

        public SaveIntoDatabaseService(ISaver saver)
        {
            this.saver = saver;
        }

        public string SaveUrl(LinkEntity entityToAdd)
        {
            if (saver.Contains(entityToAdd.Link))
            {
                return "This link already exeists in DataBase.";
            }
            var result = saver.Save(entityToAdd);
            saver.SaveChanges();
            return result;           
        }

        public void SaveUrls(List<string> listOfUrls, int iterationId)
        {
            
            var listUrlsCopy = new List<string>(listOfUrls);
            LinkEntity linkToAdd = new LinkEntity();
            foreach (string url in listUrlsCopy)
            {
                linkToAdd.Link = url;
                linkToAdd.IterationID = iterationId;
                SaveUrl(linkToAdd);
            }
            
        }
    }
}
