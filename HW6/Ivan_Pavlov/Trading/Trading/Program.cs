namespace Trading
{
    using System;
    using System.Linq;
    using Trading.Infrastructure;
    using Trading.Models;

    class Program
    {
        static void Main(string[] args)
        {
            using (AppDbContext db = new AppDbContext())
            {
                var users = db.Users.Select(u => new 
                {
         
                });
                foreach (var user in users) 
                {
                    Console.WriteLine(user.UserStocks.Count());
                }
            }

            Console.WriteLine("finish");
            Console.ReadKey();
        }
    }
}
