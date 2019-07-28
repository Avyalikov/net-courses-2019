using System.Collections.Generic;

namespace ConsoleCanvas.Interfaces
{
    public interface IDictionaryParser
    {
        Dictionary<string, string> ParseFile(string filePath);
    }
}

