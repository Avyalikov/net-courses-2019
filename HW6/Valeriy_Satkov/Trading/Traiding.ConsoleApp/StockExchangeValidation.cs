using System;

namespace Traiding.ConsoleApp
{
    public static class StockExchangeValidation
    {
        public static bool checkClientLastName(string inputString)
        {
            if (inputString.Length < 2 || inputString.Length > 20)
            {
                Console.WriteLine("Wrong Last Name. Try again.");
                return false;
            }

            return true;
        }

        public static bool checkClientFirstName(string inputString)
        {
            if (inputString.Length < 2 || inputString.Length > 20)
            {
                Console.WriteLine("Wrong First Name. Try again.");
                return false;
            }

            return true;
        }

        public static bool checkClientPhoneNumber(string inputString)
        {
            if (inputString.Length < 2 || inputString.Length > 20)
            {
                Console.WriteLine("Wrong Phone Number. Try again.");
                return false;
            }

            return true;
        }

        public static bool checkClientBalanceAmount(decimal inputDecimal)
        {
            if (inputDecimal <= 0)
            {
                Console.WriteLine("Wrong Client Balance Amount. Try again.");
                return false;
            }

            return true;
        }
    }
}
