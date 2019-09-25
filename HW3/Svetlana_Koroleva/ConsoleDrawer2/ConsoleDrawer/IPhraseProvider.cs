// <copyright file="IPhraseProvider.cs" company="SKorol">
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
    /// IPhraseProvider description
    /// </summary>
    public interface IPhraseProvider
    {
        string GetPhrase(string keyword);
        void ReadResourceFile();
    }
}
