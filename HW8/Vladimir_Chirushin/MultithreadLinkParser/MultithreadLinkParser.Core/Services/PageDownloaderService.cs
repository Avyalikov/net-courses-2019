namespace MultithreadLinkParser.Core.Services
{
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class PageDownloaderService : IPageDownloaderService
    {
        public async Task<string> DownloadPage(string urlToParse, HttpClient client, CancellationToken cts)
        {
            byte[] urlContents;
            using (var response = await client.GetAsync(urlToParse, cts))
            {
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return null;
                }
                urlContents = await response.Content.ReadAsByteArrayAsync();
            }

            var fileName = urlToParse.GetHashCode().ToString();
            File.WriteAllBytes(fileName, urlContents);
            return fileName;
        }
    }
}
