using System;
using System.Collections.Generic;
using System.Text;

namespace SiteParser.Core.Repositories
{
    public interface ICleaner
    {
        void DeleteDirectory();
        string DeleteFile(string name);
    }
}
