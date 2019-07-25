using System;
using System.Text;
using System.Collections.Generic;

namespace doors_levels
{
    public class DoorsGame
    {

        private Int32[] doors;

        private readonly IInputOutputDevice inputOutputDevice;
        private readonly IDoorsGenerator doorsGenerator;
        private readonly IDataStorage dataStorage;
        private readonly IPhraseProvider phraseProvider;
        private readonly IGameSettings gameSettings;

        public DoorsGame(
            IInputOutputDevice inputOutputDevice, 
            IDoorsGenerator doorsGenerator, 
            IDataStorage dataStorage, 
            IPhraseProvider phraseProvider, 
            IGameSettings gameSettings)
        {
            this.inputOutputDevice = inputOutputDevice;
            this.doorsGenerator = doorsGenerator;
            this.dataStorage = dataStorage;
            this.phraseProvider = phraseProvider;
            this.gameSettings = gameSettings;
            InitiateDoors();
        }


        private void InitiateDoors()
        {
            doors = doorsGenerator.GetDoors(gameSettings.GetMaxDoors());
        }


        private String ShowLevel()
        {
            StringBuilder levelString = new StringBuilder();
            for (Int32 i = 0; i < doors.Length; i++)
            {
                levelString.Append(doors[i].ToString() + " ");
            }
            return levelString.ToString();
        }

        private void ExecuteTheDoor(Int32 currentDoor)
        {
            if (currentDoor == 0)
            {
                if (!dataStorage.IsEmpty())
                {
                    Int32 lastDoor = dataStorage.GetLastDoor();
                    for (Int32 i = 0; i < doors.Length; i++)
                    {                                   
                        doors[i] /= lastDoor;
                    }
                    inputOutputDevice.WriteOutput(
                        phraseProvider.GetPhrase(Phrase.weSelectNumber) + "0" + phraseProvider.GetPhrase(Phrase.andGoPrevLevel)
                        );
                    inputOutputDevice.WriteOutput(ShowLevel());
                }
                else
                {
                    inputOutputDevice.WriteOutput(phraseProvider.GetPhrase(Phrase.itsFirstLevel));
                }
            }
            else //if door != 0
            {
                dataStorage.PushLastDoor(currentDoor);
                for (Int32 i = 0; i < doors.Length; i++)
                {
                    try
                    {
                        checked
                        {
                            doors[i] *= currentDoor;
                        }
                    }
                    catch (OverflowException)
                    {
                        inputOutputDevice.WriteOutput(phraseProvider.GetPhrase(Phrase.youGetToFar));
                        InitiateDoors();
                        dataStorage.Clear();
                        ShowDoors();
                        return;
                    }
                }
                inputOutputDevice.WriteOutput(
                        phraseProvider.GetPhrase(Phrase.weSelectNumber) + currentDoor.ToString() + phraseProvider.GetPhrase(Phrase.andGoNextLevel)
                        );
                inputOutputDevice.WriteOutput(ShowLevel());
            }
        }

        public void EnterTheDoor(Int32 doorToEnter)
        {
            if (Array.IndexOf(doors, doorToEnter) != -1)
            {
                ExecuteTheDoor(doorToEnter);
            }
            else
            {
                inputOutputDevice.WriteOutput(phraseProvider.GetPhrase(Phrase.doorDoesntExist));
            }

        }

        public void ShowDoors()
        {
            String outputMessage = phraseProvider.GetPhrase(Phrase.weHaveDoors);
            
            for (Int32 i = 0; i < doors.Length; i++)
            {
                outputMessage = outputMessage + doors[i].ToString() + " ";
            }
            inputOutputDevice.WriteOutput(outputMessage);
        }

        public void Run()
        {
            phraseProvider.InitiatePhrases();

            inputOutputDevice.WriteOutput(phraseProvider.GetPhrase(Phrase.welcome));
            ShowDoors();
            String rawUserInput;
            while (true)
            {
                rawUserInput = inputOutputDevice.ReadInput();
                if(rawUserInput.ToLower() == gameSettings.GetExitCommand().ToLower())
                {
                    break;
                }
                try
                {
                    int door = Convert.ToInt32(rawUserInput);
                    EnterTheDoor(door);
                }
                catch
                {
                    inputOutputDevice.WriteOutput(phraseProvider.GetPhrase(Phrase.pleaseEnterANumber));
                }
            }
        }
    }
}
