using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    class Program
    {
        static Stack<int> allNumbers = new Stack<int>();
        static int[] currentNumbers;
        static int numberOfValues = 5;

        static int[] getRandomNumbers()
        {
            Random rand = new Random();
            int[] numbers = new int[numberOfValues];
            bool allValuesRandom = false;
            int randCycleCounter = 0;

            do
            {
                for (int i = 0; i < numbers.Length - 1; ++i)
                {
                    numbers[i] = rand.Next(1, 10);
                }

                numbers[numbers.Length - 1] = 0;

                if (numbers.Length == numbers.Distinct().Count())
                {
                    allValuesRandom = true;
                }
            }
            while (!allValuesRandom && randCycleCounter < 1000);

            return numbers;
        }

        static void writeCurrentNumbers()
        {
            foreach (int n in currentNumbers)
            {
                Console.Write(n + " ");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            currentNumbers = getRandomNumbers();

            writeCurrentNumbers();

            bool exitCondition = false;

            while(!exitCondition)
            {
                int inputValue = 1;

                bool inputCheck = false;

                do
                {
                    try
                    {
                        string input = Console.ReadLine();
                        inputValue = Convert.ToInt32(input);

                        if (currentNumbers.Contains(inputValue))
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

                if (inputValue != 0)
                {
                    int[] tempNumbers = new int[numberOfValues];

                    try
                    {
                        for (int i = 0; i < currentNumbers.Length; ++i)
                        {
                            tempNumbers[i] = checked(currentNumbers[i] * inputValue);
                        }

                        for (int i = 0; i < currentNumbers.Length; ++i)
                        {
                            currentNumbers[i] = tempNumbers[i];
                        }

                        allNumbers.Push(inputValue);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Maximum level reached, going back");

                        if(allNumbers.Count > 0)
                        {
                            for (int i = 0; i < currentNumbers.Length; ++i)
                            {
                                currentNumbers[i] = (currentNumbers[i] / allNumbers.Peek());
                            }

                            allNumbers.Pop();
                        }  
                    }

                    writeCurrentNumbers();   
                }
                else
                {
                    if (allNumbers.Count > 0)
                    {
                        for (int i = 0; i < currentNumbers.Length; ++i)
                        {
                            currentNumbers[i] = (currentNumbers[i] / allNumbers.Peek());
                        }

                        writeCurrentNumbers();

                        allNumbers.Pop();
                    }
                    else
                    {
                        Console.WriteLine("End");
                        Console.WriteLine("Press enter to close the program");
                        Console.ReadKey();
                        exitCondition = true;
                    }
                }
            }
        }
    }
}

