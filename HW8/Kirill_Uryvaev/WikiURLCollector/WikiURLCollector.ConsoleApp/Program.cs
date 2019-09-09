using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WikiURLCollector.Core.Services;
using WikiURLCollector.Core.Models;
using StructureMap;
using System.Diagnostics;
using System.Threading;

namespace WikiURLCollector.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new WikiUrlRegistry());
            var urlCollector = container.GetInstance<ParallelUrlCollector>();
            string exitCode = "e";
            string userInput = "";
            int maxIterations = 2;
            Console.WriteLine($"{DateTime.Now} Program started");
            while (!userInput.ToLower().Equals(exitCode))
            {
                userInput = Console.ReadLine();
                urlCollector.GetUrls(userInput, maxIterations);
            }
        }

        
    }
} 
