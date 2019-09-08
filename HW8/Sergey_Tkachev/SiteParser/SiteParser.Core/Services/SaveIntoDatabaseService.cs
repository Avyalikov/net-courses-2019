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

        public string SaveUrl(string url)
        {
            var result = saver.Save(url);
            return result;           
        }
    }
}
