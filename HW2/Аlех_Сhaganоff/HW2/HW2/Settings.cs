using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyType = System.Int32;

namespace HW2
{
    public class Settings
    {
        public int NumberOfValues { get; set; }
        public int MinRand { get; set; }
        public int MaxRand { get; set; }
        public int GoBack { get; set; }

        public Settings() { NumberOfValues = 5; MinRand = 1; MaxRand = 10; GoBack = 0; }
    }
}