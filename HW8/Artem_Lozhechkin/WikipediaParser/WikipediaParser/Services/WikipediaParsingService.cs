using System;
using System.Collections.Generic;
using System.Text;
using WikipediaParser.Repositories;
using WikipediaParser.Models;
using System.Net.Http;
using System.IO;
using WikipediaParser.DTO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace WikipediaParser.Services
{
    public class WikipediaParsingService
    {
        private string baseAddress;

        public WikipediaParsingService()
        {
        }
        public void Start(string baseUrl)
        {
            this.baseAddress = baseUrl;

            LinkInfo link = new LinkInfo { Level = 0, URL = this.baseAddress };

            ProcessUrlRecursive(link);
        }
        private void ProcessUrlRecursive(LinkInfo link)
        {
            bool isSucceeded = false;
            do
            {
                if (link.Level < 3)
                {
                    try
                    {
                        link.FileName = this.DownloadSourceToFile(link);
                        List<LinkInfo> links;
                        using (UnitOfWork uof = new UnitOfWork())
                        {
                            links = this.ExtractTags(uof, link);
                            AddToDb(uof, link);
                        }
                        Parallel.ForEach(links, ProcessUrlRecursive);
                        isSucceeded = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else isSucceeded = true;
            } while (!isSucceeded);
        }

        private List<LinkInfo> ExtractTags(UnitOfWork uof, LinkInfo linkInfo)
        {
            List<LinkInfo> links = new List<LinkInfo>();
            try
            {
                using (Stream sw = new FileStream(linkInfo.FileName, mode: FileMode.Open))
                {
                    XmlReaderSettings readerSettings = new XmlReaderSettings();
                    readerSettings.DtdProcessing = DtdProcessing.Parse;
                    using (XmlReader reader = XmlReader.Create(sw, readerSettings))
                    {
                        XElement content = XElement.Load(reader);
                        foreach (XElement obs in content.Descendants("a"))
                        {
                            var link = obs.Attribute("href");
                            if (link != null && (link.Value.StartsWith("/wiki") || link.Value.Contains("/en.wikipedia.org")))
                            {
                                if (link.Value.StartsWith("/wiki"))
                                {
                                    string url = "https://en.wikipedia.org" + link.Value;
                                    if (!uof.LinksTableRepository.ContainsByUrl(new LinkEntity { Link = url }))
                                        links.Add(new LinkInfo { URL = url, Level = linkInfo.Level + 1 });
                                }
                                else if (link.Value.StartsWith("//"))
                                {
                                    string url = "https:" + link.Value;
                                    if (!uof.LinksTableRepository.ContainsByUrl(new LinkEntity { Link = url }))
                                        links.Add(new LinkInfo { URL = url, Level = linkInfo.Level + 1 });
                                }
                                else
                                {
                                    if (!uof.LinksTableRepository.ContainsByUrl(new LinkEntity { Link = link.Value }))
                                        links.Add(new LinkInfo { URL = link.Value, Level = linkInfo.Level + 1 });
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

        public string DownloadSourceToFile(LinkInfo link)
        {
            string filename = link.Level + " " + link.URL.GetHashCode() + ".html";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = client.GetAsync(link.URL).Result)
                    {
                        using (HttpContent content = response.Content)
                        {
                            using (StreamWriter file = File.CreateText(filename))
                            {
                                file.Write(content.ReadAsStringAsync().Result);
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return filename;
        }
        public void AddToDb(UnitOfWork uof, LinkInfo linkInfo)
        {
            LinkEntity linkEntity = new LinkEntity { IterationId = linkInfo.Level, Link = linkInfo.URL };
            if (!uof.LinksTableRepository.ContainsByUrl(linkEntity))
            {
                uof.LinksTableRepository.Add(linkEntity);
            }
        }

    }
}
