using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Core.Dto;
using TradingApp.Core.Services;
using TradingConsoleApp.Dependencies;

namespace TradingApp.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new TradingAppRegistry());

            var usersService = container.GetInstance <UsersService>();
            usersService.RegisterNewUser(new UserRegistrationInfo()
            {
                Name = "Dasha",
                Surname = "Fedushina",
                PhoneNumber = "1234567"
            });
        }
    }
}
