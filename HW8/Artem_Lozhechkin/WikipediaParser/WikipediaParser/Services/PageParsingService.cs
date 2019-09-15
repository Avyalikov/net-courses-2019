namespace WikipediaParser.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Linq;
    using WikipediaParser.DTO;
    using WikipediaParser.Models;
    using HtmlAgilityPack;
    using System.Linq;

    public class PageParsingService : IPageParsingService
    {
        public List<LinkInfo> ExtractTagsFromSource(IUnitOfWork uof, LinkInfo linkInfo)
        {
            HtmlDocument doc = new HtmlDocument();
            List<LinkInfo> links = new List<LinkInfo>();
            try
            {
                doc.LoadHtml(linkInfo.Content);
                foreach (var item in doc.DocumentNode.SelectNodes("//a/@href").Distinct())
                {
                    var link = item.Attributes["href"].Value;
                    if (link != null && (link.StartsWith("/wiki") || link.Contains("/en.wikipedia.org")))
                    {
                        if (link.StartsWith("/wiki"))
                        {
                            string url = "https://en.wikipedia.org" + link;
                            if (!uof.LinksTableRepository.ContainsByUrl(new LinkEntity { Link = url }))
                                links.Add(new LinkInfo { Url = url, Level = linkInfo.Level + 1 });
                        }
                        else if (link.StartsWith("//"))
                        {
                            string url = "https:" + link;
                            if (!uof.LinksTableRepository.ContainsByUrl(new LinkEntity { Link = url }))
                                links.Add(new LinkInfo { Url = url, Level = linkInfo.Level + 1 });
                        }
                        else
                        {
                            if (!uof.LinksTableRepository.ContainsByUrl(new LinkEntity { Link = link }))
                                links.Add(new LinkInfo { Url = link, Level = linkInfo.Level + 1 });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return links;
        }
        public List<LinkInfo> ExtractTagsFromFile(IUnitOfWork uof, LinkInfo linkInfo)
        {
            List<LinkInfo> links = new List<LinkInfo>();
            try
            {
                using (Stream sw = new FileStream(linkInfo.FileName, mode: FileMode.Open))
                {
                    XmlReaderSettings readerSettings = new XmlReaderSettings
                    {
                        DtdProcessing = DtdProcessing.Parse
                    };
                    using (XmlReader reader = XmlReader.Create(sw, readerSettings))
                    {
                        XElement content = XElement.Load(reader);
                        foreach (XElement obs in content.Descendants("a").Where(a => a.Attribute("href") != null).Distinct())
                        {
                            var link = obs.Attribute("href");
                            if (link != null && (link.Value.StartsWith("/wiki") || link.Value.Contains("/en.wikipedia.org")))
                            {
                                if (link.Value.StartsWith("/wiki"))
                                {
                                    string url = "https://en.wikipedia.org" + link.Value;
                                    if (!uof.LinksTableRepository.ContainsByUrl(new LinkEntity { Link = url }))
                                        links.Add(new LinkInfo { Url = url, Level = linkInfo.Level + 1 });
                                }
                                else if (link.Value.StartsWith("//"))
                                {
                                    string url = "https:" + link.Value;
                                    if (!uof.LinksTableRepository.ContainsByUrl(new LinkEntity { Link = url }))
                                        links.Add(new LinkInfo { Url = url, Level = linkInfo.Level + 1 });
                                }
                                else
                                {
                                    if (!uof.LinksTableRepository.ContainsByUrl(new LinkEntity { Link = link.Value }))
                                        links.Add(new LinkInfo { Url = link.Value, Level = linkInfo.Level + 1 });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                File.Delete(linkInfo.FileName);
            }
            return links;
        }
    }
}
