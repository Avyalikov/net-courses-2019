using System;
using System.Collections.Generic;
using System.Text;

namespace DoorsAndLevelsGame
{
    /// <summary>
    /// This class loads and contains all the Game settings.
    /// </summary>
    class Settings
    {
        /// <summary>
        /// This language field containts element of Languages enum which identifies langguage in game.
        /// </summary>
        public readonly Languages language;
        /// <summary>
        /// This field contains number of doors which should be used in game.
        /// </summary>
        public readonly int numberOfDoors;
        /// <summary>
        /// Settings constructor which initializes all the fields.
        /// </summary>
        /// <param name="language">Language for game.</param>
        /// <param name="phrases">Dictionary with phrases.</param>
        /// <param name="numberOfDoors">Number of doors.</param>
        public Settings(Languages language, int numberOfDoors)
        {
            this.numberOfDoors = numberOfDoors;
            this.language = language;
        }
    }
}
