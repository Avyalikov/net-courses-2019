namespace SiteParser.Core.Repositories
{
    public interface IDownloader
    {
        /// <summary>
        /// Downloads content from internet page.
        /// </summary>
        /// <param name="url">Url of page.</param>
        /// <returns></returns>
        string Download(string url);

        /// <summary>
        /// Saves content info file.
        /// </summary>
        /// <param name="downloadedResult">String to save.</param>
        /// <returns></returns>
        string SaveIntoFile(string downloadedResult);
    }
}
