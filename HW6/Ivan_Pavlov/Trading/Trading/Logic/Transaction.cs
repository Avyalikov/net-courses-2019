namespace Trading.Logic
{
    using System;
    using System.Linq;
    using Trading.Infrastructure;

    static class Transaction
    {

        public static void Run()
        {
            Models.User user1 = ChooseUsers();
            Models.User user2 = ChooseUsers(user1.Id);

        }

        private static Models.User ChooseUsers(int LastUserId = 0)
        {
            using (AppDbContext db = new AppDbContext())
            {
                Random random = new Random();
                int Id;
                while ((Id = random.Next(1, db.Users.Count())) == LastUserId);
                return db.Users.Find(Id);             
            }
        }      
    }
}
