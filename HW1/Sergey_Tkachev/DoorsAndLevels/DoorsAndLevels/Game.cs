using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevels
{
    class Game
    {
        int[] levelNumbers;
        Stack<int> history;
        int selectedNum;
        int maxLevel;
        int currentLevel;
        int exitNum;

        public Game()
        {
            this.levelNumbers = new int[] { 0, 0, 0, 0, 0 };
            this.history = new Stack<int>();
            this.maxLevel = 6;
            exitNum = -1;
            currentLevel = 1;
        }

        /// <summary>Fills array with unique numbers from 1 to 7 except the last element.</summary>
        /// <param name="nums">Array to fill</param>
        void FillArray(ref int[] nums)
        {
            Random random = new Random();
            int num;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                do
                {
                    num = random.Next(1, 7);
                } while (ContainsInArray(ref nums, num));
                nums[i] = num;
            }
        }

        /// <summary>Prints array into console.</summary>
        /// <param name="nums">Array of integers to print</param>
        void printArray(ref int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                Console.Write(nums[i] + " ");
            }
            Console.WriteLine(".");
        }

        /// <summary>Checks if entered number is integer, if not then number should be entered again.</summary>
        /// <returns></returns>
        int GetNum()
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
        bool ContainsInArray(ref int[] nums, int num)
        {
            for (int i = 0; i < nums.Length; i++)
                if (num == nums[i])
                    return true;
            return false;
        }

        /// <summary>Divides all elements of array to a number.</summary>
        /// <param name="nums">Array of integers</param>
        /// <param name="denominator">Denominator</param>
        void DivideArrayElements(ref int[] nums, int denominator)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] /= denominator;
            }
        }

        /// <summary>Multiplies all elements of array to a number.</summary>
        /// <param name="nums">Array of integers</param>
        /// <param name="multiplier">Multiplier</param>
        void MultiplyArrayElements(ref int[] nums, int multiplier)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] *= multiplier;
            }
        }

        public void Run()
        {

            FillArray(ref levelNumbers);

            Console.WriteLine($"Welcome to the Doors and Levels game. Enter '{exitNum}' to exit.");

            while (true)
            {
                Console.Write($"Level #{currentLevel}. There are the next doors: ");

                printArray(ref levelNumbers);

                do
                {
                    Console.Write("Select one of existing numbers: ");
                    selectedNum = GetNum();
                    if (selectedNum == exitNum)
                        break;
                } while (!ContainsInArray(ref levelNumbers, selectedNum));

                if (selectedNum == exitNum)
                {
                    Console.WriteLine("Thanks for playing!");
                    break;
                }
                else if (selectedNum == 0)
                {
                    if (currentLevel > 1)
                    {
                        DivideArrayElements(ref levelNumbers, history.Pop());
                        Console.WriteLine($"You selected {selectedNum} and go to the previous level.");
                        currentLevel--;
                    }
                    else if (currentLevel == 1) {
                        Console.WriteLine("This is a first level already. Choose another number.");
                    }
                }
                else 
                {
                    if (currentLevel < maxLevel)
                    {
                        MultiplyArrayElements(ref levelNumbers, selectedNum);
                        history.Push(selectedNum);
                        Console.WriteLine($"You selected '{selectedNum}' and go to the next level.");
                        currentLevel++;
                    }
                    else {
                       Console.WriteLine("You are on the max level now. The only way is back.");
                    }
                }

            }
        }
    }
}
