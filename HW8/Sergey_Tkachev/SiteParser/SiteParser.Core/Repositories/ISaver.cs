﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SiteParser.Core.Repositories
{
    public interface ISaver
    {
        bool Contains(string urlToCheck);

        string Save(string urlToSave);
    }
}
