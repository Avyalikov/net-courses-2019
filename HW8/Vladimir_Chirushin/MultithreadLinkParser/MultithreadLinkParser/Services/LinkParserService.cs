namespace MultithreadLinkParser.Services
{
    using HtmlAgilityPack;
    using MultithreadLinkParser.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class LinkParserService : ILinkParserService
    {
        private readonly ILinkToDBManager linkToDBManager;

        public LinkParserService(ILinkToDBManager linkToDBManager)
        {

            this.linkToDBManager = linkToDBManager;
        }

        public async Task AccessTheWebAsync(
          List<LinkInfo> linkInfo,
          List<string> urlList,
          CancellationToken cts,
          int currentLayer)
        {
            HttpClient client = new HttpClient();

            IEnumerable<Task<List<string>>> downloadTasksQuery =
                from url in urlList select ProcessURL(url, client, cts);

            List<Task<List<string>>> downloadTasks = downloadTasksQuery.ToList();

            while (downloadTasks.Count > 0)
            {
                Task<List<string>> firstFinishedTask = await Task.WhenAny(downloadTasks);

                downloadTasks.Remove(firstFinishedTask);

                int countAddedLinks = 0;
                lock (linkInfo)
                {
                    foreach (var link in firstFinishedTask.Result)
                    {
                        if (linkInfo.Where(l => l.urlString == link).FirstOrDefault() == null)
                        {
                            countAddedLinks++;
                            linkInfo.Add(new LinkInfo { urlString = link, linkLayer = currentLayer });
                        }
                    }
                }

                Thread.Sleep(10);
                Console.WriteLine($"There {countAddedLinks} more url added to link chain from layer {currentLayer}");
                linkToDBManager.RunLinkToDBAdderAsync(linkInfo, cts);
                if (currentLayer < 4)
                    AccessTheWebAsync(linkInfo, firstFinishedTask.Result, cts, currentLayer + 1);
            }
        }

        public async Task<List<string>> ProcessURL(string urlToParse, HttpClient client, CancellationToken cts)
        {
            Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;

            byte[] urlContents;
            using (var response = await client.GetAsync(urlToParse, cts))
            {
                urlContents = await response.Content.ReadAsByteArrayAsync();
            }

            // customer request write downloaded page to file
            // TODO: File Read/Write
            // var fileName = url.GetHashCode().ToString();
            // File.WriteAllBytes(fileName, content);
            // var downloadedData = File.ReadAllText(fileName);

            // delete next string when you will try to add saving page to file
            string downloadedData = Encoding.UTF8.GetString(urlContents);
            HashSet<string> urlHashSet = new HashSet<string>();

            var doc = new HtmlDocument();
            doc.LoadHtml(downloadedData);

            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//a[@href]");

            foreach (var n in nodes)
            {
                string href = n.Attributes["href"].Value;

                var uri = new Uri(href, UriKind.RelativeOrAbsolute);
                if (!uri.IsAbsoluteUri)
                    uri = new Uri(new Uri(urlToParse), uri);

                if (uri.Host == new Uri(urlToParse).Host)
                {
                    urlHashSet.Add(uri.ToString());
                }
            }

            return urlHashSet.ToList();
        }
    }
}
