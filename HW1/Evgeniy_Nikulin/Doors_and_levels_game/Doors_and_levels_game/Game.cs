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
        private readonly IInputOutputModule io;

        public Game(
            IPhraseProvider phraseProvider,
            IInputOutputModule io
            )
        {
            this.phraseProvider = phraseProvider;
            this.io = io;
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
            io.Print(phraseProvider.GetPhrase(Phrase.Welcome));
            io.Print(doors);

            // Main loop
            ulong door;
            while (true)
            {
                door = GetDoor();
                io.Print();
                if (door == 0)
                {
                    io.Print(phraseProvider.GetPhrase(Phrase.MoveBack));
                    PreviousLvl();
                } else {
                    io.Print(phraseProvider.GetPhrase(Phrase.MoveForward));
                    NextLvl(door);
                }
                if (IsWin())
                {
                    io.Print(phraseProvider.GetPhrase(Phrase.YouWin));
                    return;
                }
                io.Print(doors);
            }

        }     

        ulong GetDoor()
        {
            ulong num;
            string str;

            while (true)
            {
                io.Print(phraseProvider.GetPhrase(Phrase.YourChoose), end: "");
                str = io.Input();
                try
                {
                    num = Convert.ToUInt64(str);
                    if (doors.Contains(num))
                    {
                        return num;
                    }
                } catch { }

                io.Print(phraseProvider.GetPhrase(Phrase.WrongValue));
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