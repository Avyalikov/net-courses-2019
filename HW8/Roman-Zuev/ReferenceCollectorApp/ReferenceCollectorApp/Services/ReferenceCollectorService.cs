using HtmlAgilityPack;
using ReferenceCollectorApp.Models;
using ReferenceCollectorApp.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReferenceCollectorApp.Services
{
    public class ReferenceCollectorService : IReferenceCollectorService
    {

        private readonly HttpClient httpClient;
        private readonly IReferenceTable referenceTable;
        private readonly object filteredUrlsLock;
        private readonly object dbLock;
        private readonly object filesLock;

        public ReferenceCollectorService(IReferenceTable referenceTable)
        {
            this.referenceTable = referenceTable;
            this.httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["Uri"]);
            this.filteredUrlsLock = new object();
            this.dbLock = new object();
            this.filesLock = new object();
        }
        public async Task<string> DownloadPage(string uri, string folderPath)
        {
            Thread.Sleep(1500);
            var random = new Random();
            string fileName = "page";
            try
            {
                string responseBody = await httpClient.GetStringAsync(uri);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                lock (filesLock)
                {
                    do
                    {
                        fileName += random.Next(1000);
                    } while (File.Exists(folderPath + fileName));

                    File.WriteAllText(folderPath + fileName, responseBody, new UTF8Encoding());
                }
                return folderPath + fileName;
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public Dictionary<string,int> AddRefsToDictionary(string filePath, int iterationId/*, Dictionary<string, int> fileredUrls*/)
        {
            var innerStorage = new Dictionary<string, int>();
            if (filePath == null)
            {
                return innerStorage;
            }

            lock (filteredUrlsLock)
            {
                foreach (var item in ExtractRefs(filePath))
                {
                    if (item!=null && !innerStorage.ContainsKey(item.Attributes["href"].Value.ToLowerInvariant()))
                    {
                        //fileredUrls.Add(item.Attributes["href"].Value.ToLowerInvariant(), iterationId);
                        innerStorage.Add(item.Attributes["href"].Value.ToLowerInvariant(), iterationId);
                    }
                }
            }
            File.Delete(filePath);
            return innerStorage;
        }


        public void WriteDictionaryToDb(Dictionary<string, int> data)
        {
            if (data.Count < 1)
            {
                return;
            }

            var result = new List<ReferenceEntity>();
            foreach (var item in data)
            {
                result.Add(new ReferenceEntity
                {
                    Reference = item.Key,
                    iterationId = item.Value
                });
            }

            lock (dbLock)
            {

                foreach (var item in result)
                {
                    if (!referenceTable.ContainsById(item.Reference))
                    {
                        referenceTable.Add(item);
                    }
                }
                referenceTable.SaveChanges();
            }
        }
        private IEnumerable<HtmlNode> ExtractRefs(string filePath)
        {
            var doc = new HtmlDocument();
            doc.Load(filePath);
            return doc.DocumentNode.SelectNodes("//a[@href]").Where(c=>c.Attributes["href"].Value.StartsWith("/wiki/")
            && !c.Attributes["href"].Value.Contains(".") && !c.Attributes["href"].Value.Contains("disambiguation"));
        }

        //public List<ReferenceEntity> GetRefsList(string filePath, int iterationId)
        //{
        //    var result = new List<ReferenceEntity>();
        //    foreach (var item in ExtractRefs(filePath))
        //    {
        //        if (!result.Any(c => c.Reference == item.Attributes["href"].Value))
        //        {
        //            result.Add(new ReferenceEntity
        //            {
        //                Reference = item.Attributes["href"].Value,
        //                iterationId = iterationId
        //            });
        //        }
        //    }
        //    File.Delete(filePath);
        //    return result;
        //}

        //public void WriteToDb(List<ReferenceEntity> data)
        //{
        //    using (var mutex = new Mutex(false, "WritingToDb"))
        //    {
        //        if (data.Count > 0)
        //        {
        //            foreach (var item in data)
        //            {
        //                if (!referenceTable.ContainsById(item.Reference))
        //                {
        //                    referenceTable.Add(item);
        //                }
        //            }
        //            referenceTable.SaveChanges();
        //        }
        //    }
        //}
    }
}
