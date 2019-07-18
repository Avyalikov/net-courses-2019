﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevelsGame
{
    public class GameManager
    {
        private const int maxDoorNumber = 10;
        private int _doorsCount;
        private int[] _currentDoors;
        private int _levelNumber;
        private List<int> _pickedDoors;
        private bool isRestarting;
        private readonly IPhraseProvider phraseProvider;

        private void createRandomDoors()
        {
            Random rnd = new Random();
            _currentDoors = Enumerable.Range(1, maxDoorNumber).OrderBy(x => rnd.Next()).Take(_doorsCount).ToArray(); // for generation without duplicates
            _currentDoors[_doorsCount - 1] = 0;
            _levelNumber = 0;
            _pickedDoors = new List<int>();
        }

        private void goOnNextLevel()
        {
            for (int i = 0; i < _doorsCount; i++)
            {
                _currentDoors[i] *= _pickedDoors[_levelNumber];
                if (_currentDoors[i]<0)
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
                for (int i = 0; i < _doorsCount; i++)
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

        public GameManager(int doorCount, IPhraseProvider phraseProvider)
        {
            if (doorCount < 2)
            {
                throw new Exception("Game must contain at least two doors");
            }
            this.phraseProvider = phraseProvider;
            _doorsCount = doorCount;
            isRestarting = false;
            createRandomDoors();
        }

        public string ShowCurrentLevel()
        {
            return phraseProvider.GetPhrase("NowLevelIs") + _levelNumber.ToString() +phraseProvider.GetPhrase("DoorsAre") + string.Join(" ",_currentDoors);
        }

        public string PickDoor(int doorNumber)
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
                    return phraseProvider.GetPhrase("Restart") + ShowCurrentLevel();
                }
                else
                {
                    return ShowCurrentLevel();
                }
            }
        }
    }
}
