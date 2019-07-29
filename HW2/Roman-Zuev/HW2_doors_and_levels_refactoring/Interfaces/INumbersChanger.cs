using System.Collections.Generic;

namespace HW2_doors_and_levels_refactoring
{
	public interface INumbersChanger
    {
        int[] ChangeNumbers(int[] NumbersToChange, string ChangeValue, Stack<int> usernums);
    }
}

