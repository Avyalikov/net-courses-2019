namespace DoorsAndLevelsRefactoring
{
    using DoorsAndLevelsRefactoring.Interface;
    using System.Text;

    class GameLogic
    {
        private readonly IPhraseProvider phraseProvider;
        private readonly IInputAndOutput inputAndOutput;
        private readonly IChooseDoorsStorage doorsStorage;

        private readonly GameSettings gameSetting;
        
        private int[] Doors { get; set; }
                
        public GameLogic(
            IPhraseProvider phraseProvider,
            IInputAndOutput inputAndOutput,
            IGetDoorsNumber getDoors,
            IChooseDoorsStorage doorsStorage,
            ISettingProvider settingProvider)
        {
            this.phraseProvider = phraseProvider;
            this.inputAndOutput = inputAndOutput;
            this.doorsStorage = doorsStorage;

            this.gameSetting = settingProvider.GetGameSettings();
            this.Doors = getDoors.GetDoorsNumber(gameSetting.DoorsAmount);
        }

        public void StartGame()
        {
            inputAndOutput.WriteOutput(phraseProvider.GetPhrase("Welcome"));
            inputAndOutput.WriteOutput(phraseProvider.GetPhrase("ExitDoor") + gameSetting.ExitDoorNumber.ToString());
            inputAndOutput.WriteOutput(phraseProvider.GetPhrase("ExitCode") + gameSetting.ExitCode.ToString());

            while (true)
            {
                StringBuilder doorsForUser = new StringBuilder();
                doorsForUser.Append(phraseProvider.GetPhrase("TheNumbersAre"));
                foreach(int numb in Doors)
                {
                    doorsForUser.Append(numb);
                    doorsForUser.Append(" ");
                }
                inputAndOutput.WriteOutput(doorsForUser.ToString());
                inputAndOutput.WriteOutput(phraseProvider.GetPhrase("SelectAndEnterNumber"));

                string UserInput;
                if ((UserInput = inputAndOutput.ReadInput())
                    .ToLowerInvariant() == gameSetting.ExitCode.ToLowerInvariant())
                    break;

                NumberChanger(UserInput);

                if (WinTheGame())
                {
                    inputAndOutput.WriteOutput(phraseProvider.GetPhrase("Winner"));
                    break;
                }
            }
            inputAndOutput.WriteOutput(phraseProvider.GetPhrase("Bay"));
            inputAndOutput.ReadKeyForExit();       
        }

        private void NumberChanger(string userInput)
        {
            int Temp;
            if(!int.TryParse(userInput, out Temp))
            {
                inputAndOutput.WriteOutput(phraseProvider.GetPhrase("WrongValue"));
                return;
            }
            
            if (Temp == gameSetting.ExitDoorNumber)
                LeaveLvl();
            else if (CorrectDoor(Temp))
                NextLvl(Temp);
        }

        private bool CorrectDoor(int selectedDoor)
        {
            foreach (int door in Doors)
                if (selectedDoor == door)
                    return true;

            inputAndOutput.WriteOutput(phraseProvider.GetPhrase("OtherNumber"));
            return false;
        }

        private bool WinTheGame()
        {
            foreach(int door in Doors)
            {
                if (door > gameSetting.WinDoor || door < 0) return true;
            }

            return false;
        }

        private void NextLvl(int door)
        {
            doorsStorage.Push(door);
            for (int i = 0; i < Doors.Length; i++)
                Doors[i] = Doors[i] * door;
        }

        private void LeaveLvl()
        {
            if (doorsStorage.HasValue())
            {
                int temp = doorsStorage.Pop();
                for (int i = 0; i < Doors.Length; i++)
                {
                    if (Doors[i] == 0) continue;
                    Doors[i] = Doors[i] / temp;
                }
            }
            else
                inputAndOutput.WriteOutput(phraseProvider.GetPhrase("StorageExcept"));
        }
    }
}
