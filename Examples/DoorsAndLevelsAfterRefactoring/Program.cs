using System;
using System.Collections.Generic;

namespace DoorsAndLevelsAfterRefactoring
{

    class Program
    {
        static IPhraseProvider phraseProvider = new JsonPhraseProvider();
        static IInputOutputDevice ioDevice = new ConsoleInputOutputDevice();


        static int[] NumbersArray = StartNumsGenerator(5); // array with the current numbers
        static List<int> UserNumbers = new List<int> { 1 }; // numbers entered by user from the start except 0
        static string UserInput; // user input through io device
        static void Main()
        {
            ioDevice.WriteOutput(phraseProvider.GetPhrase("Welcome"));

            while (true)
            {
                string Numbers = phraseProvider.GetPhrase("TheNumbersAre");
                foreach (int number in NumbersArray)
                {
                    Numbers = Numbers + number + " ";
                }
                Numbers += phraseProvider.GetPhrase("SelectAndEnterNumber");
                ioDevice.WriteOutput(Numbers);
                UserInput = ioDevice.ReadInput();
                if (UserInput == "x" || UserInput == "X") break; // x - exit command
                NumbersChanger(UserInput);
            }
            ioDevice.WriteOutput(phraseProvider.GetPhrase("ThankYouForPlaying"));
            ioDevice.ReadKey();
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
                ioDevice.WriteOutput(phraseProvider.GetPhrase("YouHaveEnteredWrongValue"));
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
            ioDevice.WriteOutput(phraseProvider.GetPhrase("YouHaveEnteredWrongValuePleaseEnterNumbersListed"));
            return;
        }
    }
}
