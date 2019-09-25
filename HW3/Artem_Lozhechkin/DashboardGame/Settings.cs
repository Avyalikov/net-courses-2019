//-----------------------------------------------------------------------
// <copyright file="Settings.cs" company="AVLozhechkin">
//     Copyright (c) AVLozhechkin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DashboardGame
{
    using System;

    /// <summary>
    /// This class contains game settings.
    /// </summary>
    internal class Settings
    {
        /// <summary>
        /// Initializes a new instance of the Settings class.
        /// </summary>
        /// <param name="lang">Current language.</param>
        /// <param name="x">Width of the IBoard.</param>
        /// <param name="y">Height of the IBoard.</param>
        public Settings(Languages lang, int x, int y)
        {
            this.CurrentLanguage = lang;
            if (x < 50 & y < 30)
            {
                throw new Exception($"Размеры поля меньше допустимых: ширина - {x}, высота - {y}");
            }

            this.BoardWidth = x;
            this.BoardHeight = y;
        }

        /// <summary>
        /// Gets integer which represents a height value for initializing the board.
        /// </summary>
        public int BoardHeight { get; }

        /// <summary>
        /// Gets integer which represents a width value for initializing the board.
        /// </summary>
        public int BoardWidth { get; }

        /// <summary>
        /// Gets Languages value of language which is used for loading phrases.
        /// </summary>
        public Languages CurrentLanguage { get; }
    }
}
