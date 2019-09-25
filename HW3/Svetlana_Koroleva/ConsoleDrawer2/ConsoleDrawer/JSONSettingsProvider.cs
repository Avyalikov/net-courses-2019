// <copyright file="JSONSettingsProvidercs.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace ConsoleDrawer
{
    using System;
    using Newtonsoft.Json;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    /// <summary>
    /// JSONSettingsProvidercs description
    /// </summary>
    public class JSONSettingsProvider:ISettingsProvider
    {

        public DrawSettings GetDrawSettings()
        {
            var resourceFile = new FileInfo("Resources\\Settings.json");

            if (!resourceFile.Exists)
            {
                throw new ArgumentException($"The settings file {resourceFile.Name} doesn't exist");
            }
            var resourceFileContent = File.ReadAllText(resourceFile.FullName);
            try
            {
                return JsonConvert.DeserializeObject<DrawSettings>(resourceFileContent);


            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Can't read the settings file", ex);
            }
        }
    }
}
