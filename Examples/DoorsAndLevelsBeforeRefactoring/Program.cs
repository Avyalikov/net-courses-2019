using System;
using System.Collections.Generic;

namespace DoorsAndLevelsBeforeRefactoring
{
        class Program
        {
            static int[] NumbersArray = StartNumsGenerator(5); // array with the current numbers
            static List<int> UserNumbers = new List<int> { 1 }; // numbers entered by user from the start except 0
            static string UserInput; // user input through Console.Readline
            static void Main()
            {
                Console.WriteLine(
    @"Welcome to ""Doors and levels game beta""

You can go to a next level by choosing and entering one of the given numbers or go to a previous level by entering 0

You can quit the game by entering 'X' in the console");

                while (true)
                {
                    string Numbers = "\nThe numbers are:\n";
                    foreach (int number in NumbersArray)
                    {
                        Numbers = Numbers + number + " ";
                    }
                    Numbers += "\nSelect and enter one of the given numbers:\n";
                    Console.WriteLine(Numbers);
                    UserInput = Console.ReadLine();
                    if (UserInput == "x" || UserInput == "X") break; // x - exit command
                    NumbersChanger(UserInput);
                }
                Console.WriteLine("Thank you for playing!\nPress any button to quit");
                Console.ReadKey();
            }

            //method generates start numbers
            static int[] StartNumsGenerator(int ArraySize)
            {
                int[] nums = new int[ArraySize];
                Random random = new Random();
                //fills array with random numbers from 1 to 9
                for (int i = 0; i < nums.Length; i++)
                {
                    nums[i] = random.Next(1, 9);
                }
                //adds 0 in random place of array
                nums[random.Next(0, nums.Length - 1)] = 0;
                return nums;
            }

            //validates the input and changes the numbers in array
            static void NumbersChanger(string UserInput)
            {
                int Temp;
                // validates entered string is a number
                if (!Int32.TryParse(UserInput, out Temp))
                {
                    Console.WriteLine("You have entered a wrong value. Please enter a number");
                    return;
                }
                // goint to previous level
                if (Temp == 0)
                {
                    for (int i = 0; i < NumbersArray.Length; i++)
                    {
                        NumbersArray[i] /= UserNumbers[UserNumbers.Count - 1];
                    }
                    if (UserNumbers.Count > 1) UserNumbers.RemoveAt(UserNumbers.Count - 1);
                    return;
                }
                //validating entered number
                foreach (int number in NumbersArray)
                {
                    //if value contains in the array -> multiply array numbers
                    if (number == Temp)
                    {
                        for (int i = 0; i < NumbersArray.Length; i++) NumbersArray[i] *= Temp;
                        UserNumbers.Add(Temp); // adding value to the List
                        return;
                    }
                }
                //if array doesn't contain entered number
                Console.WriteLine("You have entered a wrong number. Please enter one of the numbers listed");
                return;
            }
        }
}
