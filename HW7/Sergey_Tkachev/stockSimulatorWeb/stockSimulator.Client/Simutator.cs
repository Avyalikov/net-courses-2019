using stockSimulator.Core.DTO;
using stockSimulator.Core.Services;
using StructureMap;
using System;

namespace stockSimulator.Client
{
    internal class Simutator
    {
        const int numberOfFunctions = 10;
        const int firstFunction = 1;
        const int exitCode = -1;

        public Simutator()
        {
        }

        internal void start()
        {
            ClientRequests clientRequests = new ClientRequests();
            int userChoise;
            do
            {
                ShowMenu();
                Console.WriteLine("Choose one of numbers or print '-1' to exit: ");
                userChoise = GetNum(firstFunction, numberOfFunctions);
                switch (userChoise)
                {
                    case 1:
                        clientRequests.ShowListOfClients();
                        break;
                    default:
                        break;
                }
            } while (userChoise != exitCode);
        }

        private void ShowMenu()
        {
            Console.WriteLine(@"This application provides next functions:
1 - Show list of first 'n' clients.
2 - Add new client.
3 - Update client.
4 - Remove client.
5 - Show client's stocks.
6 - Add new stock to client.
7 - Update client's stock.
8 - Remove client's stock.
9 - Show list of 'n' client's transactions.
10 - Make a new deal between clients.");
        }

        internal void stop()
        {
            throw new NotImplementedException();
        }

        public static int GetNum(int min = -2147483648, int max = 2147483647)
        {
            while (true)
                if (!int.TryParse(Console.ReadLine(), out int enteredNum) && ((enteredNum >=min && enteredNum <= max) || enteredNum == exitCode))
                    Console.Write("Incorrect input. Please try again: ");
                else return enteredNum;
        }
    }
}