namespace ReferenceCollector.Core.Services
{
    using HtmlAgilityPack;
    using ReferenceCollector.Core.Repositories;
    using System;
    using System.Collections.Generic;
    public abstract class HtmlParser
    {
        private readonly IFileSystemRepository fileSystemRepository;

        public HtmlParser(IFileSystemRepository fileSystemRepository)
        {
            this.fileSystemRepository = fileSystemRepository;
        }
        public IEnumerable<HtmlNode> ParseFromFile (string filePath)
        {
            if (!fileSystemRepository.FileExists(filePath))
            {
                throw new ArgumentException($"File doesn't exist. Trying to find it here: {filePath}");
            }

            var doc = new HtmlDocument();
            doc.Load(filePath);
            return Parse(doc);
        }

        public IEnumerable<HtmlNode> ParseFromString(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentException($"The data string is null or empty");
            }

            var doc = new HtmlDocument();
            doc.LoadHtml(data);
            return Parse(doc);
        }

        public IEnumerable<HtmlNode> ParseFromWeb(string uri)
        {
            if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            {
                throw new ArgumentException($"Uri has invalid scheme");
            }

            var web = new HtmlWeb();
            var doc = web.Load(uri);
            return Parse(doc);
        }
        public abstract IEnumerable<HtmlNode> Parse(HtmlDocument doc);
    }
}
