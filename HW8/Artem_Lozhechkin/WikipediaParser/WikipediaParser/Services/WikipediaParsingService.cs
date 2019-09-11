using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using WikipediaParser.DTO;
using WikipediaParser.Models;

namespace WikipediaParser.Services
{
    public class WikipediaParsingService
    {
        private string baseAddress;
        private readonly DownloadingService downloadingService;
        private readonly PageParsingService pageParsingService;
        private readonly DatasourceManagementService datasourceManagementService;

        public WikipediaParsingService(
            DownloadingService downloadingService, 
            PageParsingService pageParsingService,
            DatasourceManagementService datasourceManagementService)
        {
            this.downloadingService = downloadingService;
            this.pageParsingService = pageParsingService;
            this.datasourceManagementService = datasourceManagementService;
        }
        public void Start(string baseUrl)
        {
            this.baseAddress = baseUrl;

            LinkInfo link = new LinkInfo { Level = 0, URL = this.baseAddress };

            ProcessUrlRecursive(link).Wait();
        }
        private async Task ProcessUrlRecursive(LinkInfo link)
        {
            bool isSucceeded = false;
            do
            {
                if (link.Level < 2 && isSucceeded == false)
                {
                    try
                    {
                        link.FileName = await this.downloadingService.DownloadSourceToFile(link);
                        List<LinkInfo> links;
                        using (UnitOfWork uof = new UnitOfWork())
                        {
                            links = await this.pageParsingService.ExtractTagsFromFile(uof, link);
                            await this.datasourceManagementService.AddToDb(uof, link);
                        }
                        List<Task> t = new List<Task>();
                        foreach (var item in links)
                        {
                            t.Add(Task.Run(() => ProcessUrlRecursive(item)));
                        }
                        
                        await Task.WhenAll(t.ToArray());
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
    }
}
