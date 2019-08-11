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
                var users = db.Users.Include("UserStocks");
                               
                foreach (var user in users)
                {
                    Console.WriteLine(user.ToString());
                }
            }

            Console.WriteLine("finish");
            Console.ReadKey();
        }
    }
}
