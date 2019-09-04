using HW8.Classes;
using HW8.Intefaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace HW8
{
    class Program
    {
        

        static void Main(string[] args)
        {
            IInputProvider inputProvider = new ConsoleInputProvider();
            IOutputProvider outputProvider = new ConsoleOutputProvider();
            //IStorageProvider storageProvider = new LocalStorageProvider();
            IStorageProvider storageProvider = new EFStorageProvider();
            IPageProvider pageProvider = new WebClinetPageProvider();
            ILinkProcessorProvider linkProcessorProvider = new LinkProcessor(outputProvider, storageProvider);
            IPageParserProvider pageParserProvider = new PageParserProvider();
            
            WebPageProcessor wpp = new WebPageProcessor(storageProvider, inputProvider, outputProvider, pageProvider, linkProcessorProvider, pageParserProvider);

            wpp.Start();

            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(500);
                    Console.WriteLine(wpp.numberOfThreadsRunning);
                }
            });

            Console.ReadLine();
        }
    }
}
