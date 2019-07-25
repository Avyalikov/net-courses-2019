using System;
using System.Collections.Generic;

namespace doors_levels
{
    public interface IFileParser
    {
        Dictionary<String, String> ParseFile(String FilePath);
    }
}

