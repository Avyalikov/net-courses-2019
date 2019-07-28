// <copyright file="ISettingsProvidercs.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace ConsoleDrawer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// ISettingsProvidercs description
    /// </summary>
    public interface ISettingsProvider
    {
        DrawSettings GetDrawSettings();
    }
}
