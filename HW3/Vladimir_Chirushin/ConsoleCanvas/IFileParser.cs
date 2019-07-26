using System;
using System.Collections.Generic;

namespace ConsoleCanvas
{
    public interface IFileParser
        {
            Dictionary<String, String> ParseFile(String FilePath);
        }
    }

