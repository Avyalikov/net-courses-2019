using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevelsGame
{
    public class GameManager
    {
        private int[] _currentDoors;
        private Stack<int> _pickedDoors;
        private bool isRestarting;

        private GameSettings _settings;

        private readonly IPhraseProvider phraseProvider;
        private readonly IInputOutputProvider inputOutputProvider;
        private readonly INumberGenerator numberGenerator;
        private readonly ISettingsProvider settingsProvider;

        public GameManager(IPhraseProvider phraseProvider, IInputOutputProvider inputOutputProvider, INumberGenerator numberGenerator, ISettingsProvider settingsProvider)
        {
            this.phraseProvider = phraseProvider;
            this.inputOutputProvider = inputOutputProvider;
            this.numberGenerator = numberGenerator;
            this.settingsProvider = settingsProvider;

            _settings = settingsProvider.GetSettings();
            phraseProvider.SetLanguage(_settings.LanguageFile);
            isRestarting = false;          
        }

        public void Run()
        {
            createRandomDoors();
            string rules = phraseProvider.GetPhrase("Rules");
            rules = rules.Replace("@ExitCode", _settings.ExitString);
            rules = rules.Replace("@ExitDoor", _settings.ExitDoorNumber.ToString());
            inputOutputProvider.Write(rules);
            inputOutputProvider.Write(showCurrentLevel());
            string key = "";
            int pickedDoor = 0;
            while (!key.ToLower().Equals(_settings.ExitString))
            {
                key = inputOutputProvider.Read();
                bool isNumeric = int.TryParse(key, out pickedDoor);
                if (isNumeric)
                {
                    inputOutputProvider.Write(pickDoor(pickedDoor));
                }
                else if (!key.ToLower().Equals(_settings.ExitString))
                {
                    inputOutputProvider.Write(phraseProvider.GetPhrase("IncorrectInput"));
                }
            }
        }

        private void createRandomDoors()
        {
            _currentDoors = numberGenerator.GetNumbers(_settings.DoorsNumber, _settings.MaxDoorNumber, _settings.ExitDoorNumber);
            _pickedDoors = new Stack<int>();
        }

        private void goOnNextLevel()
        {
            for (int i = 0; i < _settings.DoorsNumber-1; i++)
            {
                _currentDoors[i] *= _pickedDoors.First();
                if (_currentDoors[i] < 0)
                {
                    isRestarting = true;
                    return;
                }
            }
        }

        private void goOnPreviousLevel()
        {
            if (_pickedDoors.Count > 0)
            {
                for (int i = 0; i < _settings.DoorsNumber-1; i++)
                {
                    _currentDoors[i] /= _pickedDoors.First();
                }
                _pickedDoors.Pop();
            }
            else
            {
                createRandomDoors();
            }
        }

        private string showCurrentLevel()
        {
            return phraseProvider.GetPhrase("NowLevelIs") + _pickedDoors.Count.ToString() + phraseProvider.GetPhrase("DoorsAre") + string.Join(" ", _currentDoors);
        }

        private string pickDoor(int doorNumber)
        {
            if (!_currentDoors.Contains(doorNumber))
            {
                return phraseProvider.GetPhrase("DoorAbsent");
            }
            else
            {
                if (doorNumber != _settings.ExitDoorNumber)
                {
                    _pickedDoors.Push(doorNumber);
                    goOnNextLevel();
                }
                else
                {
                    goOnPreviousLevel();
                }
                if (isRestarting)
                {
                    isRestarting = false;
                    createRandomDoors();
                    return phraseProvider.GetPhrase("Restart") + showCurrentLevel();
                }
                else
                {
                    return showCurrentLevel();
                }
            }
        }
    }
}
