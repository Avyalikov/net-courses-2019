namespace ReferenceCollectorApp.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using HtmlAgilityPack;
    using ReferenceCollector.Core.Repositories;
    using ReferenceCollector.Core.Services;
    public class WikiReferencesParsingService : HtmlParser, IWikiReferencesParsingService
    {
        public WikiReferencesParsingService(IFileSystemRepository fileSystemRepository) : base(fileSystemRepository)
        {
        }
        public override IEnumerable<HtmlNode> Parse(HtmlDocument doc)
        {
            return doc.DocumentNode.SelectNodes("//a[@href]").Where(c => c.Attributes["href"].Value.StartsWith("/wiki/")
            && !c.Attributes["href"].Value.Contains(".") && !c.Attributes["href"].Value.Contains("disambiguation"));
        }

        public Dictionary<string, int> ParseRefsFromFileToDictionary(string filePath, int iterationId)
        {
            var innerStorage = new Dictionary<string, int>();
            if (filePath == null)
            {
                return innerStorage;
            }

            foreach (var item in ParseFromFile(filePath))
            {
                if (item != null && !innerStorage.ContainsKey(item.Attributes["href"].Value.ToLowerInvariant()))
                {
                    innerStorage.Add(item.Attributes["href"].Value.ToLowerInvariant(), iterationId);
                }
            }

            return innerStorage;
        }
    }
}
