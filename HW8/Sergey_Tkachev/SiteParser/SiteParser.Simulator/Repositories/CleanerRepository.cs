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
        public void DeleteDirectory()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;

            string fullpath = Path.Combine(path, folderName);
            if (Directory.Exists(fullpath))
            {
                Directory.Delete(fullpath);
            }

        }

        public string DeleteFile(string path)
        {
            string result = string.Empty;
            try
            {
                File.Delete(path);
            } catch (FileNotFoundException ex)
            {
                result = $"File {path} doesn't found. " + ex.Message;
                return result;
            }
            result = $"File {path} was deleted.";
            return result;
        }
    }
}
