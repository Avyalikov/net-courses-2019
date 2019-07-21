using System;
using System.Collections.Generic;

namespace Doors_and_levels_game
{
    class Program
    {
        static Game game = new Game();
        static void Main(string[] args)
        {
            game.Start();
        }
    }

    public class Game
    {
        Random rnd = new Random();
        Stack<ulong> chosenDoors = new Stack<ulong>();
        List<ulong> doors = new List<ulong>();

        public void Start()
        {
            // Initiation random doors
            for (int i = 0; i < 4; i++)
            {
                while (true)
                {
                    ulong r = (ulong)rnd.Next(2, 9);
                    if (!doors.Contains(r))
                    {
                        doors.Add(r);
                        break;
                    }
                }
            }
            doors.Add(0);

            // Start
            Console.WriteLine("The game of doors\n");
            Console.WriteLine("Welcome in my castle, you must choose the right door.");
            PrintArr(doors);

            // Main loop
            byte n = 0;
            ulong door;
            while (true)
            {
                door = GetDoor();
                Console.WriteLine();

                if (door == 0)
                {
                    if (n != 0) n--;
                    Console.WriteLine("You return to the previouse doors");

                    PreviousLvl();
                }
                else
                {
                    n++;
                    Ends end = (n == 1 || n == 2 || n == 3) ? (Ends)n : Ends.th;
                    Console.WriteLine($"You move forward throught {n}{end} door");

                    NextLvl(door);
                }

                if (IsWin())
                {
                    Console.WriteLine($"\nYou win! your score is: {door} after {n} doors");
                    return;
                }

                PrintArr(doors);
            }

        }
        enum Ends { th, st, nd, rd }
        void PrintArr(List<ulong> arr)
        {
            foreach (ulong item in arr)
            {
                Console.Write(item + " ");
            }
            Console.Write("\n");
        }

        ulong GetDoor()
        {
            ulong num;
            string str;

            while (true)
            {
                Console.Write("Your choose: ");
                str = Console.ReadLine();
                try
                {
                    num = Convert.ToUInt64(str);
                    if (doors.Contains(num))
                    {
                        return num;
                    }
                } catch { }

                Console.WriteLine("Look, do you see door like this?");
                Console.WriteLine("So do i, try again.");
            }
        } 

        void NextLvl(ulong door)
        {
            chosenDoors.Push(door);
            for (int i = 0; i < doors.Count; i++)
            {
                doors[i] *= door;
            }
        }
        void PreviousLvl()
        {
            if (chosenDoors.TryPop(out ulong door))
            {
                for (int i = 0; i < doors.Count; i++)
                {
                    doors[i] /= door;
                }
            }
        }

        bool IsWin()
        {
            for (byte i = 0; i < 4; i++)
            {
                if (doors[i] == 0)
                {                    
                    return true;
                }
            }
            return false;
        }
    }
}