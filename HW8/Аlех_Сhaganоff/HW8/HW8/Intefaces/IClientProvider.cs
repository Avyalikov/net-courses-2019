using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW8.Intefaces
{
    public interface IClientProvider
    {
        int PageDownloadCounter { get; set; }

        string DownloadString(string url);

        void DownloadFile(string url, string filename);
    }
}
