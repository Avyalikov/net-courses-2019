using System;
using System.Collections.Generic;
using Doors_and_levels_game.Interfaces;

namespace Doors_and_levels_game
{
    public class Game
    {
        Random rnd = new Random();
        Stack<ulong> chosenDoors = new Stack<ulong>();
        List<ulong> doors = new List<ulong>();

        private readonly IPhraseProvider phraseProvider;

        public Game(IPhraseProvider phraseProvider)
        {
            this.phraseProvider = phraseProvider;
        }

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
            Console.Write(phraseProvider.GetPhrase(Phrase.Welcome));
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
                    Console.WriteLine(phraseProvider.GetPhrase(Phrase.MoveBack));

                    PreviousLvl();
                }
                else
                {
                    n++;
                    Ends end = (n == 1 || n == 2 || n == 3) ? (Ends)n : Ends.th;
                    Console.WriteLine(phraseProvider.GetPhrase(Phrase.MoveForward));

                    NextLvl(door);
                }

                if (IsWin())
                {
                    Console.WriteLine(phraseProvider.GetPhrase(Phrase.YouWin));
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
                Console.Write(phraseProvider.GetPhrase(Phrase.YourChoose));
                str = Console.ReadLine();
                try
                {
                    num = Convert.ToUInt64(str);
                    if (doors.Contains(num))
                    {
                        return num;
                    }
                } catch { }

                Console.WriteLine(phraseProvider.GetPhrase(Phrase.WrongValue));
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