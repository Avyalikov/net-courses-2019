using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doors_and_levels_game
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> doorNumbersArr = new List<int>();
            //List<string> strNumbersArr= new List<string>();
            int userNumber;
            Random r = new Random();
            List<int> userArr = new List<int>();
            string userInput="";
            /*function to generate new List from existing List by multiplication*/
            void cnslWriteNewLst(List<int> newLst, string operation, int multNum, List<int> extraLst)
            {
                for (int i = 0; i < newLst.Count; i++)
                {
                    int num = newLst[i];
                    if (operation == "mult")
                    {
                        int newNumber = num * multNum;
                        newLst[i] = newNumber;
                    }
                    else if (operation == "div")
                    {
                        int divNum = extraLst.LastOrDefault();
                        try
                        {
                            int numD = num / divNum;
                            newLst[i] = numD;
                        }
                        catch
                        {
                            Console.WriteLine("Division by zero happened");
                        }
                    }
                    Console.Write(newLst[i] + " ");
                }
                Console.WriteLine();
            }
            /*prints four random numbers and zero to the console*/
            Console.WriteLine("Please select a number from first string and enter it to the console, or 'exit' to quit programm");
            for (int i = 0; i < 5; i++)
            {
                doorNumbersArr.Add(r.Next(10));
            }
            doorNumbersArr[r.Next(4)]=0;
            for(int i=0; i < doorNumbersArr.Count; i++)
            {
                Console.Write(doorNumbersArr[i] + " ");
            }
            Console.WriteLine();
            /*user enters a value*/
            while (userInput != "exit")
            {
                userInput = Console.ReadLine();
                if (userInput == "0")   /*if user entered zero*/
                {
                    if (userArr.Count == 0)  /*if it is a first valid enter*/
                    {
                        Console.WriteLine(@"This is the first level.
Please select a number greater then 0 and enter it to the console");
                        for (int i = 0; i < doorNumbersArr.Count; i++)
                        {
                            Console.Write(doorNumbersArr[i] + " ");
                        }
                        Console.WriteLine();
                    }
                    else  /*if it is not a first valid enter, programm should divide every number of array to turn back to the upper level*/
                    {

                        cnslWriteNewLst(doorNumbersArr, "div", 1, userArr);
                        userArr.Remove(userArr[userArr.Count - 1]);
                    }
                }
                Boolean IsSucceeded = int.TryParse(userInput, out int t);
                if (IsSucceeded)  /*if user entered a number*/
                    {
                        if (doorNumbersArr.Contains(t))   /*if it is a valid number*/
                        {
                            int v = int.Parse(userInput);
                            userNumber = v;
                            cnslWriteNewLst(doorNumbersArr, "mult", userNumber, null);
                            userArr.Add(userNumber);
                        }
                        else
                        {
                        Console.WriteLine("Please choose from the proposed numbers"); 

                        }
                } 
                else   /*if user entered not a number*/
                {
                    Console.WriteLine("Please select a number and enter it to the console");
                }
            }
        }
    }
}