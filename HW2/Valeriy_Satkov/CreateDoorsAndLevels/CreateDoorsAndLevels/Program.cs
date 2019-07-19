using System;
using System.Collections.Generic;

namespace CreateDoorsAndLevels
{
    class Program
    {
        static void Main()
        {
            new Game { }.Run();

            Console.WriteLine("Good Bye.");
            Console.ReadKey();
        }        
    }

    class Game
    {
        List<int> numbers;        
        List<int> selectedNumbers;
        int levelLimit;

        enum Operation
        {
            NextLevel,
            PrevLevel,
            Print
        }

        public void Run()
        {
            // int[] numbers = new int[] { 2, 4, 3, 1, 0 }); // simple test
            this.numbers = RandIntArrGenerator();
            this.selectedNumbers = new List<int>();
            this.levelLimit = 2;            

            // Console.WriteLine($"We have numbers: 2, 4, 3, 1, 0");            
            Console.WriteLine(StringFromNumbersArr(intro: "We have numbers: ", operation: Operation.Print));

            do
            {
                this.selectedNumbers.Add(EnterTheNumber()); // Select one of the current level numbers or type 'exit'

                if (this.selectedNumbers[this.selectedNumbers.Count - 1] == -1) break; // if -1 then break the circle - EXIT

                if (this.selectedNumbers[this.selectedNumbers.Count - 1 ] > 0)
                {
                    // Create "next level" numbers where numbers values calculate using formula: [number on previous level] * [choosed number on previous level].
                    // "We select number 2 and go to next level: 4 8 6 2 0 (2x2 4x2 3x2 1x2 0x2)"
                    Console.WriteLine(StringFromNumbersArr(intro: $"We select number {this.selectedNumbers[this.selectedNumbers.Count - 1]} and go to next level: ", operation: Operation.NextLevel));
                }
                else if (this.selectedNumbers.Count - 1 > 0) // selectedNumber == 0, level > 0
                {
                    this.selectedNumbers.RemoveAt(this.selectedNumbers.Count - 1); // remove 0 from selectedNumbers. Now top is prev.number
                                                                                   // "We select number 0 and go to previous level: 4 8 6 2 0"
                    Console.WriteLine(StringFromNumbersArr(intro: "We select number 0 and go to previous level: ", operation: Operation.PrevLevel));
                    this.selectedNumbers.RemoveAt(this.selectedNumbers.Count - 1); // remove prev.number from selectedNumbers. Now top is prev2.number
                }
            } while (!(this.selectedNumbers.Count - 1 == 0 && this.selectedNumbers[0] == 0));
        }

        private List<int> RandIntArrGenerator()
        {
            List<int> result = new List<int>();
            int randomNumber;

            Random random = new Random();

            for (int i = 0; i < 4; i++)
            {
                while (result.Contains(randomNumber = random.Next(1, 9)));
                result.Add(randomNumber);
            }

            result.Add(0);

            return result;
        }

        private int EnterTheNumber()
        {
            int result = -1;

            do
            {
                if (this.selectedNumbers.Count <= this.levelLimit)
                {
                    Console.Write("Select one of the current level numbers or type 'exit': ");
                }
                else
                {
                    Console.Write("You are on the last level. Enter 0 for continue or type 'exit': ");
                }

                try
                {
                    string s = Console.ReadLine();

                    if (String.IsNullOrEmpty(s))
                    {
                        Console.WriteLine("You entered empty string. Try again.");
                        continue;
                    }

                    if (s.ToLower().Equals("exit")) break; // if 'exit' then break the circle. result will be -1

                    int n = Int32.Parse(s);

                    if (!this.numbers.Contains(n))
                    {
                        Console.WriteLine("Wrong number. Try again.");
                        continue;
                    }

                    if (this.selectedNumbers.Count > this.levelLimit && n != 0)
                    {
                        Console.WriteLine("You entered not 0.");
                        continue;
                    }

                    result = n;
                }
                catch(OverflowException)
                {
                    Console.WriteLine("Toooooo large number. Try another one.");
                    continue;
                }
                catch
                {
                    Console.WriteLine("Input error. Try again.");
                    continue;
                }
            } while (result == -1);

            return result;
        }

        private string StringFromNumbersArr(string intro, Operation operation)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(intro);
            for (int i = 0; i < numbers.Count; i++)
            {
                switch (operation)
                {
                    case Operation.NextLevel:
                        sb.Append(numbers[i] *= this.selectedNumbers[this.selectedNumbers.Count - 1]);
                        break;
                    case Operation.PrevLevel:
                        sb.Append(numbers[i] /= this.selectedNumbers[this.selectedNumbers.Count - 1]);
                        break;
                    case Operation.Print:
                        sb.Append(numbers[i]);
                        break;
                    default:
                        break;
                }
                sb.Append(i < numbers.Count - 1 ? " " : String.Empty); // add space between them
            }

            if (operation == Operation.NextLevel) // info about next level
            {
                sb.Append(" (");
                for (int i = 0; i < numbers.Count; i++)
                {
                    sb.Append($"{numbers[i] / this.selectedNumbers[this.selectedNumbers.Count - 1]}x{this.selectedNumbers[this.selectedNumbers.Count - 1]}");
                    sb.Append(i < numbers.Count - 1 ? " " : String.Empty); // add space between them
                }
                sb.Append(")");
            }

            return sb.ToString();
        }
    }
}
