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
    public class WebPageProcessor
    {
        private string startingUrl;
        private int recursionLimit;
        private int threadSleepTime;
        private IStorageProvider storageProvider;
        private IInputProvider inputProvider;
        private IOutputProvider outputProvider;
        private IPageProvider pageProvider;
        private ILinkProcessorProvider linkProcessorProvider;
        private IPageParserProvider pageParserProvider;

        public long numberOfThreadsRunning;

        private object storageLock = new object();
        private object threadcounterLock = new object();

        public WebPageProcessor
        (//string startingUrl, 
         //int recursionLimit,
         IStorageProvider storageProvider,
         IInputProvider inputProvider,
         IOutputProvider outputProvider,
         IPageProvider pageProvider,
         ILinkProcessorProvider linkProcessorProvider,
         IPageParserProvider pageParserProvider,
         int threadSleepTime)
        {
            //this.startingUrl = startingUrl;
            //this.recursionLimit = recursionLimit;
            this.storageProvider = storageProvider;
            this.inputProvider = inputProvider;
            this.outputProvider = outputProvider;
            this.pageProvider = pageProvider;
            this.linkProcessorProvider = linkProcessorProvider;
            this.pageParserProvider = pageParserProvider;
            this.threadSleepTime = threadSleepTime;

            this.recursionLimit = 2;
        }

        public void Start()
        {
            GetStartingUrl();
            GetRecursionLimit();
            GetClearOnStartSetting();

            Task.Run(() =>
            {
                if (startingUrl.EndsWith(@"/"))
                {
                    startingUrl = startingUrl.Substring(0, startingUrl.Length - 1);
                }

                ProcessWebPage(startingUrl, 0);
            });
        }

        public void ProcessWebPage(string url, int recursionLevel)
        {
            string data = pageProvider.GetPage(url);

            List<string> links = pageParserProvider.GetLinks(data);

            if (links != null)
            {
                Parallel.ForEach(links, (element) =>
                {
                    string result = linkProcessorProvider.ProcessLink(element, recursionLevel, startingUrl, storageLock);

                    if (result != null && recursionLevel < recursionLimit)
                    {
                        Thread.Sleep(threadSleepTime);

                        Task.Run(() =>
                        {
                            lock (threadcounterLock) { numberOfThreadsRunning++; }

                            ProcessWebPage(result, recursionLevel + 1);

                            lock (threadcounterLock) { numberOfThreadsRunning--; }
                        });
                    }
                });
            }
        }

        void GetStartingUrl()
        {
            string inputString = string.Empty;
            bool inputCheck = true;
            Uri uriResult;

            outputProvider.WriteLine("Please enter URL");
            do
            {
                inputString = inputProvider.ReadLine();
                inputCheck = Uri.TryCreate(inputString, UriKind.Absolute, out uriResult)&& (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                
                if(inputCheck == false)
                {
                    outputProvider.WriteLine("Incorrect URL format");
                    outputProvider.WriteLine("Please enter avsolute URL");
                }
            }
            while (inputCheck == false);

            startingUrl = inputString;
        }

        void GetRecursionLimit()
        {
            bool inputCheck = false;
            int inputValue = 0;

            outputProvider.WriteLine("Please enter recursion limit");

            do
            {
                try
                {
                    string inputString = inputProvider.ReadLine();
                    inputValue = Convert.ToInt32(inputString);
                    inputCheck = true;
                }
                catch (Exception)
                {
                    outputProvider.WriteLine("Incorrect input");
                    outputProvider.WriteLine("Please enter a single integer value");
                }
            }
            while (inputCheck == false);

            recursionLimit = inputValue;
        }

        void GetClearOnStartSetting()
        {
            int inputValue = 0;
            bool inputCheck = true;

            Console.WriteLine("Clear exisitng database?");
            Console.WriteLine("1 - yes, 0 - no");

            do
            {
                try
                {
                    string input = Console.ReadLine();
                    inputValue = Convert.ToInt32(input);

                    if (!(inputValue == 0 || inputValue == 1))
                    {
                        inputCheck = false;
                        Console.WriteLine("Please choose 1 or 0");
                    }
                    else
                    {
                        inputCheck = true;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Incorrect input");
                    Console.WriteLine("Please enter a single integer value");
                }
            }
            while (inputCheck == false);

            if(inputValue == 1)
            {
                storageProvider.Clear();
            }
        }
    }
}
