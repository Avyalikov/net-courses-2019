using System;
using System.Collections.Generic;

namespace Game
{
    class Program
    {
        /// <summary>Fills array with unique numbers from 1 to 7 except the last element.</summary>
        /// <param name="nums">Array to fill</param>
        static void FillArray(ref int[] nums)
        {
            Random random = new Random();
            int num;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                do{
                    num = random.Next(1, 7);
                } while (ContainsInArray(ref nums, num));
                nums[i] = num;
            }
        }

        /// <summary>Prints array into console.</summary>
        /// <param name="nums">Array of integers to print</param>
        static void printArray(ref int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                Console.Write(nums[i] + " ");
            }
            Console.WriteLine(".");
        }

        /// <summary>Checks if entered number is integer, if not then number should be entered again.</summary>
        /// <returns></returns>
        static int GetNum()
        {
            while (true)
                if (!int.TryParse(Console.ReadLine(), out int enteredNum))
                    Console.Write("Incorrect input. Please try again: ");
                else return enteredNum;
        }

        /// <summary>Checks if array contains a number as an element.</summary>
        /// <param name="nums">Array to check</param>
        /// <param name="num">Number to find</param>
        /// <returns></returns>
        static bool ContainsInArray(ref int[] nums, int num)
        {
            for (int i = 0; i < nums.Length; i++)
                if (num == nums[i])
                    return true;
            return false;
        }

        /// <summary>Divides all elements of array to a number.</summary>
        /// <param name="nums">Array of integers</param>
        /// <param name="denominator">Denominator</param>
        static void DivideArrayElements(ref int[] nums, int denominator)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] /= denominator;
            }
        }

        /// <summary>Multiplies all elements of array to a number.</summary>
        /// <param name="nums">Array of integers</param>
        /// <param name="multiplier">Multiplier</param>
        static void MultiplyArrayElements(ref int[] nums, int multiplier)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] *= multiplier;
            }
        }


        static void Main(string[] args)
        {
            int[] levelNumbers = { 0, 0, 0, 0, 0 };
            Stack<int> history = new Stack<int>();
            int selectedNum;

            FillArray(ref levelNumbers);

            Console.WriteLine("Welcome to the Doors and Levels game.");

            while (true)
            {
                Console.Write("There are the next numbers: ");

                printArray(ref levelNumbers);

                do{
                    Console.Write("Select one of existing numbers: ");
                    selectedNum = GetNum();

                } while (!ContainsInArray(ref levelNumbers, selectedNum));

                if(selectedNum == 0)
                {
                    if (history.Count > 0)
                    {
                        DivideArrayElements(ref levelNumbers, history.Pop());
                        Console.WriteLine($"You selected {selectedNum} and go to the previous level.");
                    }
                    else
                        Console.WriteLine("This is a first level already. Choose another number.");
                }
                else
                {
                    MultiplyArrayElements(ref levelNumbers, selectedNum);
                    history.Push(selectedNum);
                    Console.WriteLine($"You selected {selectedNum} and go to the next level.");
                }

            }
        }
    }
}
