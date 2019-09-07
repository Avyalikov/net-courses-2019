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
    using System.Threading;
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
            Console.WriteLine("Enter the url:");
            // string inputString = Console.ReadLine();

            //string inputString = "https://en.wikipedia.org/wiki/The_Mummy_(1999_film)"; // TEST

            //string[] startPageHost = new string[3];
            //int dotOrgPos = inputString.IndexOf(".org");
            //startPageHost[0] = inputString.Substring(0, dotOrgPos + 4); // https://en.wikipedia.org
            //startPageHost[1] = inputString.Substring(6, dotOrgPos - 2); // //en.wikipedia.org
            //startPageHost[2] = inputString.Substring(dotOrgPos + 4, 6); // /wiki/

            string inputString = "https://gameofthrones.fandom.com/wiki/Jon_Snow"; // TEST

            string[] startPageHost = new string[2];
            int dotComPos = inputString.IndexOf(".com");
            startPageHost[0] = inputString.Substring(0, dotComPos + 4); // https://gameofthrones.fandom.com
            startPageHost[1] = inputString.Substring(dotComPos + 4, 6); // /wiki/

            

            string path = @"LinkFiles";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            CancellationTokenSource parseCancelTokenSource = new CancellationTokenSource(30000);
            CancellationToken parseCancellationToken = parseCancelTokenSource.Token;

            int firstIterationId = 0;

            try
            {
                // Save link to DB and get her Id
                parsingService.Save(inputString, firstIterationId);
            }
            catch (ArgumentException e)
            {
                // If find link in DB, write message
                parseCancelTokenSource.Cancel();
                var message = e.Message;
            }            

            Task parseTask = new Task(() => parsingService.ParsingLinksByIterationId(firstIterationId, startPageHost, parseCancellationToken));

            Console.WriteLine("Press 'Enter' and stay away:");
            Console.ReadKey(); // pause

            parseTask.Start();

            //Thread.Sleep(10000);
            //parseCancelTokenSource.Cancel();

            parseTask.Wait();

            Console.WriteLine("The End.");
            Console.ReadKey();            
        }
    }
}
