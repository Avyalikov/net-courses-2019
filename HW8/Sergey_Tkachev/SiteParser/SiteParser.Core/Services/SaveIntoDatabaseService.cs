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
            var result = saver.Save(entityToAdd);
            return result;           
        }

        public void SaveUrls(List<string> listOfUrls, int iterationId)
        {
            LinkEntity linkToAdd = new LinkEntity();
            foreach (string url in listOfUrls)
            {
                linkToAdd.Link = url;
                linkToAdd.IterationID = iterationId;
                SaveUrl(linkToAdd);
            }
        }
    }
}
