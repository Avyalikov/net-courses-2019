namespace MultithreadApp
{
    using MultithreadApp.Core.Services;
    using MultithreadApp.Repo;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static int i = 0;
        static void Main(string[] args)
        {
            string URL = "https://en.wikipedia.org/wiki/The_Mummy_(1999_film)";
            Console.WriteLine("Подключение к дб.....");
            
            int count = 0;
            while (count != 5)
            {
                Runner(URL);
                count++;
            }

            Console.WriteLine("Тест окончен");
            Console.ReadKey();
        }

        private static void Runner(string URL)
        {
            var linksService = new LinksServices(new LinksTableRepo(new LinksDbContext()));
            linksService.RunSingle(URL);
            var links = linksService.GetLinks();
            linksService = null;
            foreach(var link in links)
            {
                Task task = new Task(() =>
                {
                    linksService = new LinksServices(new LinksTableRepo(new LinksDbContext()));
                    linksService.RunSingle(link.URL);
                });
                task.Start();
                Console.WriteLine("{0}", i++);
                Thread.Sleep(1);
            }
        }
    }
}
