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

            int iterations = 0;
            Task task = Task.Factory.StartNew(() =>
            {


                while (iterations != 2)
                {
                    Runner(URL);
                    iterations++;
                }
            });
            task.Wait();
            Console.WriteLine("Тест окончен");
            Console.ReadKey();
        }

        private static void Runner(string URL)
        {
            var linksService = new LinksServices(new LinksTableRepo(new LinksDbContext()));
            linksService.RunSingle(URL);
            var links = linksService.GetLinks();
            linksService = null;
            Task startTask = new Task(() =>
            {
                foreach (var link in links)
                {
                    Task t = Task.Factory.StartNew(() =>
                    {
                        linksService = new LinksServices(new LinksTableRepo(new LinksDbContext()));
                        linksService.RunSingle(link.URL);
                        Thread.Sleep(1);
                    });                          
                    Console.WriteLine("{0}", i++);
                }
            });
            startTask.Start();
            startTask.Wait();
        }
    }
}
