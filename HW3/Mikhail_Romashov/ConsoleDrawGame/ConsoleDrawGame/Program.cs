//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Epam">
//     Copyright (c) Epam. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace ConsoleDrawGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Interfaces;

    /// <summary>
    /// This class contains an entry point.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// This method is an entry point.
        /// </summary>
        private static void Main()
        {
            ISettingsProvider settingsProvider = new SettingsProvider();
            GameSettings gameSettings = settingsProvider.GameSettings();
            Console.ReadKey();
        }
    }
}
