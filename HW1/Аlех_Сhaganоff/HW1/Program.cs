using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    class Program
    {
        static Stack<int[]> allNumbers = new Stack<int[]>();
        static int[] currentNumbers;

        static int[] getRandomNumbers()
        {
            Random rand = new Random();
            int[] numbers = new int[5];

            for (int i = 0; i<numbers.Length-1; ++i)
            {
                numbers[i] = rand.Next(1,10);
            }

            numbers[numbers.Length-1] = 0;

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
            allNumbers.Push((int[])getRandomNumbers().Clone());

            currentNumbers = (int[])allNumbers.Peek().Clone();

            writeCurrentNumbers();

            while (allNumbers.Count!=0)
            {
                int inputValue = 0;

                bool inputCheck = false;

                do
                {
                    try
                    {
                        string input = Console.ReadLine();
                        inputValue = Convert.ToInt32(input);
                        
                        if(allNumbers.Peek().Contains(inputValue))
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
                               
                if(inputValue!=0)
                {
                    try
                    {  
                        for(int i = 0; i< currentNumbers.Length; ++i)
                        {
                            currentNumbers[i] = checked(currentNumbers[i] * inputValue);
                        }
                    }
                    catch(Exception)
                    {
                        Console.WriteLine("Maximum number reached, all numbers will be reset to zero");
                        currentNumbers = new int[5];
                    } 

                    writeCurrentNumbers();

                    allNumbers.Push((int[])currentNumbers.Clone());
                }
                else
                {
                    allNumbers.Pop();
                    
                    if (allNumbers.Count>0)
                    {
                        currentNumbers = (int[])allNumbers.Peek().Clone();
                        writeCurrentNumbers();
                    }
                    else
                    {
                        Console.WriteLine("End");
                        Console.WriteLine("Press enter to close the program");
                        Console.ReadKey();
                    }
                }                
            }                       
        }
    }
}
