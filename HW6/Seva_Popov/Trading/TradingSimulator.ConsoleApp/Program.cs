using StructureMap;
using System;
using TradingSimulator.ConsoleApp.Dependency;
using TradingSimulator.ConsoleApp.Repositories;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Services;

namespace TradingSimulator.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new TradingSimulatorRegistry());
            var userService = container.GetInstance<UserService>();

            userService.RegisterNewUser(new UserRegistrationInfo()
            {
                Name = "Denis",
                Surname = "Chesnokov",
                Phone = "12345"
            });

            var sharesService = container.GetInstance<SharesService>();

            sharesService.RegisterNewShares(new SharesRegistrationInfo()
            {
                Name = "ROSN",
                Price = 410
            });



            //    //User user1 = new User { Surname = "Менделеев", Name = "Дмитрий", Balance = 1000, Phone = "777-77-77" };
            //    //User user2 = new User { Surname = "Циолковский", Name = "Константин", Balance = 2000, Phone = "555-55-55" };
            //    //User user3 = new User { Surname = "Пирогов", Name = "Николай", Balance = 3000, Phone = "999-99-99" };
            //    //User user4 = new User { Surname = "Королёв", Name = "Сергей", Balance = 4000, Phone = "111-11-11" };
            //    //db.Users.AddRange(new List<User> { user1, user2, user3, user4 });

        }
    }
}
