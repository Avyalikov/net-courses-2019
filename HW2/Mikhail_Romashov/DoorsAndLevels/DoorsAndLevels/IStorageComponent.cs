using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevels
{
    interface IStorageComponent
    {
        int GetSize();
        int Pop();
        void Push(int value);
        void Clear();
    }
}
