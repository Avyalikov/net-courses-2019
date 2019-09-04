using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW8.Intefaces
{
    public interface IPageParserProvider
    {
        List<string> GetLinks(string data);
    }
}
