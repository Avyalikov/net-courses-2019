namespace Multithread.ConsoleApp
{
    using Multithread.Core.Services;
    using StructureMap;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class ConnectedLinksParser
    {
        private readonly Container connectedLinksRegistryContainer;
        private readonly ParsingService parsingService;

        public ConnectedLinksParser(Container connectedLinksRegistryContainer)
        {
            this.connectedLinksRegistryContainer = connectedLinksRegistryContainer;
            this.parsingService = this.connectedLinksRegistryContainer.GetInstance<ParsingService>();
        }

        public void Start()
        {
            Console.WriteLine("Enter the url and stay away:");
            // string inputString = Console.ReadLine();

            //string inputString = "https://en.wikipedia.org/wiki/Mitichi"; // TEST

            //string[] startPageHost = new string[3];
            //int dotOrgPos = inputString.IndexOf(".org");
            //startPageHost[0] = inputString.Substring(0, dotOrgPos + 4); // https://en.wikipedia.org
            //startPageHost[1] = inputString.Substring(6, dotOrgPos - 2); // //en.wikipedia.org
            //startPageHost[2] = inputString.Substring(dotOrgPos + 4, 6); // /wiki/

            string inputString = "https://gameofthrones.fandom.com/wiki/Jon_Snow"; // TEST

            string[] startPageHost = new string[1];
            int dotComPos = inputString.IndexOf(".com");
            string mainPageHost = inputString.Substring(0, dotComPos + 4); // https://gameofthrones.fandom.com
            startPageHost[0] = inputString.Substring(dotComPos + 4, 6); // /wiki/

            HttpClientHandler defaultClientHandler = new HttpClientHandler();
            defaultClientHandler.UseDefaultCredentials = true;

            string path = @"LinkFiles";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            int id = 0;

            Task<string> linkContentPath = parsingService.DownloadPage(inputString, defaultClientHandler, id);
            string htmlContentPath = linkContentPath.Result;

            List<string> links = parsingService.ExtractLinksFromHtmlString(startPageHost, htmlContentPath);

            Console.ReadKey();
        }
    }
}
