using SiteParser.Core.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteParser.Simulator.Repositories
{
    class CleanerRepository : ICleaner
    {
        private string folderName = "Pages";
        public void Clean()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;

            string fullpath = Path.Combine(path, folderName);
            if (Directory.Exists(fullpath))
            {
                Directory.Delete(fullpath);
            }

        }
    }
}
