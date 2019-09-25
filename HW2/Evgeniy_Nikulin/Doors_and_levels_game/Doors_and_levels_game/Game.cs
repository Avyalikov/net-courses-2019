    using System.Collections.Generic;
    using System.IO;
    using System;
    using Interfaces;
    using Newtonsoft.Json;

    namespace Doors_and_levels_game {
        public class Game {
            private readonly GameSettings settings;
            private readonly IPhraseProvider phraseProvider;
            private readonly InputOutputModule io;
            private readonly IDoorsGenerater<List<ulong>> doorsGenerater;
            private readonly IStorageModule<ulong, List<ulong>> st;
            public Game(
                GameSettings settings,
                IPhraseProvider phraseProvider,
                InputOutputModule io,
                IDoorsGenerater<List<ulong>> doorsGenerater,
                IStorageModule<ulong, List<ulong>> storage
            ) {
                this.settings = settings;
                this.phraseProvider = phraseProvider;
                this.io = io;
                this.doorsGenerater = doorsGenerater;
                this.st = storage;
            }

            public void Start() {
                // Initiation random doors
                st.Doors = doorsGenerater.Generate(settings.doorsAmount);

                // Start
                io.Print(phraseProvider.GetPhrase(Phrase.Welcome));
                io.Print(st.Doors);

                // Main loop
                ulong door;
                int lvl = 1;
                while (true) {
                    door = GetDoor();
                    io.Print();
                    if (door == 0) {
                        io.Print(phraseProvider.GetPhrase(Phrase.MoveBack));
                        PreviousLvl();
                        if (lvl > 1) lvl--;
                    } else {
                        io.Print(phraseProvider.GetPhrase(Phrase.MoveForward));
                        NextLvl(door);
                        lvl++;
                    }

                    if (lvl > settings.maxLvL) {
                        io.Print(phraseProvider.GetPhrase(Phrase.YouWin));
                        return;
                    }

                    io.Print(st.Doors);
                }
            }

            ulong GetDoor() {
                ulong num;
                string str;

                while (true) {
                    io.Print(phraseProvider.GetPhrase(Phrase.YourChoose), end: "");
                    str = io.Input();
                    try {
                        num = Convert.ToUInt64(str);
                        if (st.Doors.Contains(num)) {
                            return num;
                        }
                    } catch { }

                    io.Print(phraseProvider.GetPhrase(Phrase.WrongValue));
                }
            }

            void NextLvl(ulong door) {
                st.PushChosenDoor(door);
                for (int i = 0; i < st.Doors.Count; i++) {
                    st.Doors[i] *= door;
                }
            }
            void PreviousLvl() {
                if (!st.ChosenDoorIsEmpty()) {
                    ulong door = st.PopChosenDoor();
                    for (int i = 0; i < st.Doors.Count; i++) {
                        st.Doors[i] /= door;
                    }
                }
            }
        }
    }