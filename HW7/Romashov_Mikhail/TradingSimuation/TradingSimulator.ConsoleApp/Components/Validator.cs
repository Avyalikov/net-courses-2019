using System;
using System.Linq;
using TradingSimulator.ConsoleApp.Interfaces;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Services;

namespace TradingSimulator.ConsoleApp.Components
{
    public class Validator : IValidator
    {
        private readonly TradersService tradersService;
        private readonly StockService stockService;

        public Validator(TradersService tradersService, StockService stockService)
        {
            this.tradersService = tradersService;
            this.stockService = stockService;
        }
        public bool TraderInfoValidate(TraderInfo traderInfo)
        {
            bool validFirstName = traderInfo.Name.All(c => char.IsLetter(c));
            if (!validFirstName)
            {
                Console.WriteLine("Wrong first name. Operation cancel.");
                return false;
            }

            bool validLastName = traderInfo.Surname.All(c => char.IsLetter(c));
            if (!validLastName)
            {
                Console.WriteLine("Wrong last name. Operation cancel.");
                return false;
            }

            bool validPhone = traderInfo.PhoneNumber.All(c => char.IsDigit(c)) && traderInfo.PhoneNumber.Length < 9;
            if (!validPhone)
            {
                Console.WriteLine("Wrong phone. Operation cancel.");
                return false;
            }
            return true;
        }

        public bool StockToTraderValidate(string trader, string stock)
        {
            return tradersService.ContainsByName(trader) && stockService.ContainsByName(stock);
        }
    }
}