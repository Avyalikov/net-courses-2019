using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteParser.Simulator
{
    public interface ISimulator
    {
        void Start(string startPageToParse, string baseUrl);
    }
}
