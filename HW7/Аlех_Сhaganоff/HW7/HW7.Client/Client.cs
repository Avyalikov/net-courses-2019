using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HW7.Client
{
    public class Client
    {
        private readonly IRequestsProvider requests;
        private readonly TradingSimulation tradingSimulation;

        public Client(IRequestsProvider requests)
        {
            this.requests = requests;
            tradingSimulation = new TradingSimulation("http://localhost:5000/");
        }

        public void Run()
        {
            bool exitCondition = false;
            int userChoice = 0;
            List<int> menuChoices = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 0 };
            
            requests.CheckConnection(requests.ConnectionString);

            do
            {
                DisplayMenu();
                userChoice = 0;

                userChoice = ProcessUserInput(menuChoices);

                if (userChoice == 0)
                {
                    exitCondition = true;
                }
                else
                {
                    CallFunction(userChoice);
                }
            }
            while (!exitCondition);
        }

        public void CallFunction(int userChoice)
        {
            switch (userChoice)
            {
                case 1: requests.GetListOfClients(); break;
                case 2: requests.AddClient(); break;
                case 3: requests.UpdateClient(); break;
                case 4: requests.RemoveClient(); break;
                case 5: requests.GetListOfShares();  break;
                case 6: requests.AddShare(); break;
                case 7: requests.UpdateShare(); break;
                case 8: requests.RemoveShare(); break;
                case 9: requests.GetBalance(); break;
                case 10: requests.GetTransactions(); break;
                case 11: requests.MakeDeal(); break;
                case 12: SimulationHandler(); break;
            }
        }

        public int ProcessUserInput(List<int> menuChoices)
        {
            int inputValue = 0;
            bool inputCheck = true;

            do
            {
                try
                {
                    string input = Console.ReadLine();
                    inputValue = Convert.ToInt32(input);

                    if (menuChoices.Contains(inputValue))
                    {
                        inputCheck = true;
                    }
                    else
                    {
                        Console.WriteLine("Please choose one of the numbers on the screen");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Incorrect input");
                    Console.WriteLine("Please enter a single integer value");
                }
            }
            while (inputCheck == false);

            return inputValue;
        }

        public void DisplayMenu()
        {
            Console.WriteLine("1-Get list of clients");
            Console.WriteLine("2-Add client");
            Console.WriteLine("3-Update client");
            Console.WriteLine("4-Remove client");
            Console.WriteLine("5-Get list of shares");
            Console.WriteLine("6-Add share");
            Console.WriteLine("7-Update share");
            Console.WriteLine("8-Remove share");
            Console.WriteLine("9-Get balance");
            Console.WriteLine("10-Get transactions");
            Console.WriteLine("11-Make deal");
            Console.WriteLine("12-Start/stop simulation");
            Console.WriteLine("0-Exit");
        }

        public void SimulationHandler()
        {
            if (tradingSimulation.SimulationIsWorking == false)
            {
                tradingSimulation.SimulationIsWorking = true;
                Console.WriteLine("Simulation started");
                RunTradingSimulation();
            }
            else
            {
                tradingSimulation.SimulationIsWorking = false;
                Console.WriteLine("Simulation ended");
            }
        }

        public void RunTradingSimulation()
        {
           Task t = Task.Run(() =>
           {
                while (tradingSimulation.SimulationIsWorking)
                {
                    var result = tradingSimulation.PerformRandomOperation();

                    requests.MakeDeal(result);

                    for (int j = 1; j < 100 && tradingSimulation.SimulationIsWorking; j++)
                    {
                        if (tradingSimulation.SimulationIsWorking)
                        {
                            Thread.Sleep(10);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            });
        }
    }
}
