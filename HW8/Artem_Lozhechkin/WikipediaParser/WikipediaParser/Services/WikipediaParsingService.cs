namespace WikipediaParser.Services
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Threading.Tasks;
    using WikipediaParser.DTO;

    public class WikipediaParsingService : IWikipediaParsingService
    {
        private string baseAddress;
        private static readonly int maxIteration = int.Parse(ConfigurationManager.AppSettings.Get("maxIteration"));
        private readonly IDownloadingService downloadingService;
        private readonly IPageParsingService pageParsingService;
        private readonly IDatasourceManagementService datasourceManagementService;

        public WikipediaParsingService(
            IDownloadingService downloadingService,
            IPageParsingService pageParsingService,
            IDatasourceManagementService datasourceManagementService)
        {
            this.downloadingService = downloadingService;
            this.pageParsingService = pageParsingService;
            this.datasourceManagementService = datasourceManagementService;
        }
        public void Start(string baseUrl)
        {
            this.baseAddress = baseUrl;

            LinkInfo link = new LinkInfo { Level = 0, Url = this.baseAddress };

            ProcessUrlRecursive(link).Wait();
        }
        public async Task ProcessUrlRecursive(LinkInfo link)
        {
            List<LinkInfo> links;

            if (link.Level < maxIteration)
            {
                try
                {
                    link.FileName = await this.downloadingService.DownloadSourceToFile(link);
                    using (UnitOfWork uof = new UnitOfWork())
                    {
                        links = this.pageParsingService.ExtractTagsFromFile(uof, link);
                        await this.datasourceManagementService.AddToDb(uof, link);
                    }
                    List<Task> t = new List<Task>();
                    foreach (var item in links)
                    {
                        t.Add(Task.Run(() => ProcessUrlRecursive(item)));
                    }
                    
                    await Task.WhenAll(t);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
