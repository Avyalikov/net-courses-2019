using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadApp.Core.Repositories
{
    public interface IExtractHtmlTags
    {
        List<string> ExtractTags(string filename);
    }
}
