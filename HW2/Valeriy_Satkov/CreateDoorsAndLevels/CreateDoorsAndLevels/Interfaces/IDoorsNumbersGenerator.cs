using System.Collections.Generic;

namespace CreateDoorsAndLevels.Interfaces
{
    interface IDoorsNumbersGenerator
    {
        List<int> generateDoorsNumbers(int doorsAmount);
    }
}
