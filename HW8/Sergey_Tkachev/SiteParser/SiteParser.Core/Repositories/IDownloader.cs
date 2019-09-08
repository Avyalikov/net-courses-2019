using System;
using System.Collections.Generic;
using System.Text;

namespace SiteParser.Core.Repositories
{
    public interface IDownloader
    {
        string Download(string url);
        string SaveIntoFile(string downloadedResult);
    }
}
