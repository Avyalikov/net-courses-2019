namespace SiteParser.Simulator.Repositories
{
    using System;
    using System.IO;
    using SiteParser.Core.Repositories;

    internal class CleanerRepository : ICleaner
    {
        private string folderName = "Pages";

        /// <summary>
        /// Deletes a directory.
        /// </summary>
        public void DeleteDirectory()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;

            string fullpath = Path.Combine(path, this.folderName);
            if (Directory.Exists(fullpath))
            {
                Directory.Delete(fullpath);
            }
        }

        /// <summary>
        /// Deletes file by path.
        /// </summary>
        /// <param name="path">Path of file to delete.</param>
        /// <returns></returns>
        public string DeleteFile(string path)
        {
            string result = string.Empty;
            try
            {
                File.Delete(path);
            }
            catch (FileNotFoundException ex)
            {
                result = $"File {path} doesn't found. " + ex.Message;
                return result;
            }

            result = $"File {path} was deleted.";
            return result;
        }
    }
}
