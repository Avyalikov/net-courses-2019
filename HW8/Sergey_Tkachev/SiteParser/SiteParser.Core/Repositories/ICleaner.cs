namespace SiteParser.Core.Repositories
{
    public interface ICleaner
    {
        /// <summary>
        /// Deletes directory.
        /// </summary>
        void DeleteDirectory();

        /// <summary>
        /// Deletes file by name.
        /// </summary>
        /// <param name="name">Name of file.</param>
        /// <returns></returns>
        string DeleteFile(string name);
    }
}
