using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WikiURLCollector.Core.Services;
using WikiURLCollector.Core.Models;
using StructureMap;

namespace WikiURLCollector.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new WikiUrlRegistry());
            var parsingService = container.GetInstance<UrlParsingService>();
            var urlService = container.GetInstance<UrlService>();
            string exitCode = "e";
            string userInput = "";
            Console.WriteLine($"{DateTime.Now} Program started");
            while (!userInput.ToLower().Equals(exitCode))
            {
                userInput = Console.ReadLine();
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = client.GetAsync(userInput).Result)
                    {
                        Console.WriteLine(response.StatusCode);
                        if (response.IsSuccessStatusCode)
                        {
                           var result = LoadPage(response, userInput, parsingService);
                            foreach (var url in result.Result)
                            {
                                urlService.AddUrl(url);
                            }
                        }
                    }
                }
            }
        }

        static async Task<IEnumerable<UrlEntity>> LoadPage(HttpResponseMessage response, string adress, UrlParsingService parsingService)
        {
            var pageContents = await response.Content.ReadAsStringAsync();
            var result = parsingService.ExtractAllUrlsFromPage(pageContents,1);
            return result;
        }
    }
}
