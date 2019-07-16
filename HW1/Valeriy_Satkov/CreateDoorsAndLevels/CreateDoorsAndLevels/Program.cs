using System;
using System.Collections.Generic;

namespace CreateDoorsAndLevels
{
    class Program
    {
        private int[] RandIntArrGenerator()
        {
            int[] result = new int[5];
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            Random random = new Random();

            for (int i = 0; i < result.Length - 1; i++)
            {
                int indexForDel = 0;

                do
                {
                    result[i] = random.Next(1, 9);
                    indexForDel = Array.IndexOf<int>(numbers, result[i]);
                } while (indexForDel == -1);
                numbers[indexForDel] = 0;
            }

            result[4] = 0;

            return result;
        }

        public void Start()
        {
            List<int[]> list = new List<int[]>();

            // list.Add(new int[] { 2, 4, 3, 1, 0 }); // simple test
            list.Add(RandIntArrGenerator());
            int selectedNumber = 0;
            int level = 0;

            Console.WriteLine($"We have numbers: {list[0][0]} {list[0][1]} {list[0][2]} {list[0][3]} {list[0][4]}");

            do
            {
                Console.Write("Enter the number: ");

                try
                {
                    selectedNumber = Int32.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Wrong number. Try again.");
                    continue;
                }

                level = list.Count - 1; // current level position

                if (selectedNumber > 0)
                {
                    // Create "next level" numbers where numbers values calculate using formula: [number on previous level] * [choosed number on previous level].
                    list.Add(new int[] {
                    list[level][0] * selectedNumber,
                    list[level][1] * selectedNumber,
                    list[level][2] * selectedNumber,
                    list[level][3] * selectedNumber,
                    list[level][4] * selectedNumber });

                    // "We select number 2 and go to next level: 4 8 6 2 0 (2x2 4x2 3x2 1x2 0x2)"
                    Console.WriteLine($"We select number {selectedNumber} and go to next level: {list[level + 1][0]} {list[level + 1][1]} {list[level + 1][2]} {list[level + 1][3]} {list[level + 1][4]} ({list[level][0]}x{selectedNumber} {list[level][1]}x{selectedNumber} {list[level][2]}x{selectedNumber} {list[level][3]}x{selectedNumber} {list[level][4]}x{selectedNumber})");
                }
                else if (level > 0)
                {
                    list.RemoveAt(level); // remove level                    

                    // "We select number 0 and go to previous level: 4 8 6 2 0"
                    Console.WriteLine($"We select number 0 and go to previous level: {list[level - 1][0]} {list[level - 1][1]} {list[level - 1][2]} {list[level - 1][3]} {list[level - 1][4]}");
                }
            } while (!(selectedNumber == 0 && level == 0));
        }
        static void Main(string[] args)
        {
            new Program { }.Start();

            Console.WriteLine("Good Bye.");
            Console.ReadKey();
        }
    }
}
