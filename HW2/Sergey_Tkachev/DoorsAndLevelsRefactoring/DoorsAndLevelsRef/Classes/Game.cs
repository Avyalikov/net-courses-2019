using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevelsRef
{
    class Game
    {
        private readonly IPhraseProvider phraseProvider;
        private readonly IInputOutput io;
        private readonly ISettingsProvider settingsProvider;
        private readonly IArrayGenerator arrayGenerator;

        private readonly GameSettings gameSettings;

        private int[] NumbersArray;
        private List<int> UserNumbers;

        ///--------------------------
        int[] levelNumbers;
        Stack<int> history;
        int selectedNum;
        int maxLevel;
        int currentLevel;
        int exitNum;

        public Game(IPhraseProvider phraseProvider,
                    IInputOutput io,
                    ISettingsProvider settingsProvider,
                    IArrayGenerator arrayGenerator)
        {
            this.phraseProvider = phraseProvider;
            this.io = io;
            this.settingsProvider = settingsProvider;
            this.arrayGenerator = arrayGenerator;

            this.gameSettings = this.settingsProvider.GetGameSettings();
            ///----------------------
            this.levelNumbers = new int[] { 0, 0, 0, 0, 0 };
            this.history = new Stack<int>();
            this.maxLevel = 6;
            exitNum = -1;
            currentLevel = 1;
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
