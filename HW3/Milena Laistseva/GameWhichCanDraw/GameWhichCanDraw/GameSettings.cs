// <copyright file="Gamesettings.cs" company="Epam">
// Copyright (c) Epam. All rights reserved.
// </copyright>

namespace GameWhichCanDraw
{
    /// <summary>
    /// Game settings
    /// </summary>
    public class GameSettings
    {
        /// <summary>
        /// Gets or sets exit code
        /// </summary>
        public string ExitCode { get; set; }

        /// <summary>
        /// Gets or sets game language
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets board length
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets board width
        /// </summary>
        public int Width { get; set; }
    }
}
