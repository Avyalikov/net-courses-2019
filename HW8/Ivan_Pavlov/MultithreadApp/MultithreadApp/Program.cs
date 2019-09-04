namespace MultithreadApp
{
    using MultithreadApp.Core.Services;
    using MultithreadApp.Repo;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            string URL = "https://en.wikipedia.org/wiki/The_Mummy_(1999_film)";
            Console.WriteLine("Подключение к дб.....");

            var linksService = new LinksServices(new LinksTableRepo(new LinksDbContext()));
            int i = 0;
            while(i != 10)
            {
                linksService.RunMulti(URL);
                i++;               
            }

            Console.WriteLine("Тест окончен");
            Console.ReadKey();
        }
    }
}
