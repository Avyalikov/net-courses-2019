namespace SiteParser.Core.Services
{
    using SiteParser.Core.Repositories;

    public class DeleteFileService
    {
        private readonly ICleaner cleaner;

        public DeleteFileService(ICleaner cleaner)
        {
            this.cleaner = cleaner;
        }

        /// <summary>
        /// Deletes file by name.
        /// </summary>
        /// <param name="fileName">Name of file.</param>
        /// <returns></returns>
        public string DeleteFile(string fileName)
        {
            var result = this.cleaner.DeleteFile(fileName);
            return result;
        }
    }
}
