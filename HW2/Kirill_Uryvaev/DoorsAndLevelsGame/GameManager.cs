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
        private int _levelNumber;
        private List<int> _pickedDoors;
        private bool isRestarting;

        private GameSettings _settings;

        private readonly IPhraseProvider phraseProvider;
        private readonly IInputOutputProvider inputOutputProvider;
        private readonly INumberGenerator numberGenerator;
        private readonly ISettingsProvider settingsProvider;

        private void createRandomDoors()
        {
            _currentDoors = numberGenerator.GetNumbers(_settings.DoorsNumber, _settings.MaxDoorNumber);
            _levelNumber = 0;
            _pickedDoors = new List<int>();
        }

        private void goOnNextLevel()
        {
            for (int i = 0; i < _settings.DoorsNumber; i++)
            {
                _currentDoors[i] *= _pickedDoors[_levelNumber];
                if (_currentDoors[i] < 0)
                {
                    isRestarting = true;
                    return;
                }
            }
            _levelNumber++;
        }

        private void goOnPreviousLevel()
        {
            if (_levelNumber > 0)
            {
                _levelNumber--;
                for (int i = 0; i < _settings.DoorsNumber; i++)
                {
                    _currentDoors[i] /= _pickedDoors[_levelNumber];
                }
                _pickedDoors.RemoveAt(_levelNumber);
            }
            else
            {
                createRandomDoors();
            }
        }

        public GameManager(IPhraseProvider phraseProvider, IInputOutputProvider inputOutputProvider, INumberGenerator numberGenerator, ISettingsProvider settingsProvider)
        {
            this.phraseProvider = phraseProvider;
            this.inputOutputProvider = inputOutputProvider;
            this.numberGenerator = numberGenerator;
            this.settingsProvider = settingsProvider;

            _settings = settingsProvider.GetSettings();
            phraseProvider.SetLanguage(_settings.Language);
            isRestarting = false;
            createRandomDoors();
        }

        private string showCurrentLevel()
        {
            return phraseProvider.GetPhrase("NowLevelIs") + _levelNumber.ToString() + phraseProvider.GetPhrase("DoorsAre") + string.Join(" ", _currentDoors);
        }

        private string pickDoor(int doorNumber)
        {
            if (!_currentDoors.Contains(doorNumber))
            {
                return phraseProvider.GetPhrase("DoorAbsent");
            }
            else
            {
                if (doorNumber != 0)
                {
                    _pickedDoors.Add(doorNumber);
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


        public void Run()
        {
            inputOutputProvider.Write(phraseProvider.GetPhrase("Rules"));
            inputOutputProvider.Write(showCurrentLevel());
            string key = "";
            int pickedDoor = 0;
            while (!key.Equals("e"))
            {
                key = inputOutputProvider.Read();
                bool isNumeric = int.TryParse(key, out pickedDoor);
                if (isNumeric)
                {
                    inputOutputProvider.Write(pickDoor(pickedDoor));
                }
                else if (!key.ToLower().Equals("e"))
                {
                    inputOutputProvider.Write(phraseProvider.GetPhrase("IncorrectInput"));
                }
            }
        }
    }

}
