﻿namespace Multithread.ConsoleApp
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

            CancellationTokenSource parseCancelTokenSource = new CancellationTokenSource(); //new CancellationTokenSource(20000);
            CancellationToken parseCancellationToken = parseCancelTokenSource.Token;

            int firstId = 0;

            try
            {
                // Save link to DB and get her Id
                firstId = parsingService.Save(inputString, 0);
            }
            catch (ArgumentException e)
            {
                // If find link in DB, write message
                parseCancelTokenSource.Cancel();
                var message = e.Message;
            }            

            Task parseTask = new Task(() => parsingService.ParsingLinksByIterationId(inputString, firstId, startPageHost, parseCancellationToken));

            Console.WriteLine("Press 'Enter' and stay away:");
            Console.ReadKey(); // pause

            parseTask.Start();
            Console.WriteLine(" Wait for it to destroy your SSD with too many requests");

            Thread.Sleep(30000);            
            parseCancelTokenSource.Cancel();
            Console.WriteLine("Now it is trying to stop. Please wait until it finished correctly");

            parseTask.Wait();

            Console.WriteLine("The End.");
            var duplicatesList = parsingService.LookingForDuplicatesInDb();
            Console.WriteLine("Duplicates: ");
            foreach (var duplicateString in duplicatesList)
            {
                Console.WriteLine($"{duplicateString.Key} — {duplicateString.Value}");
            }
            Console.WriteLine("Thats all.");
            Console.ReadKey();            
        }
    }
}
