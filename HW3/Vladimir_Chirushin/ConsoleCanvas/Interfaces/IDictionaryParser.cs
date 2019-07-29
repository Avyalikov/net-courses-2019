namespace ConsoleCanvas.Interfaces
{
    using System.Collections.Generic;

    public interface IDictionaryParser
    {
        Dictionary<string, string> ParseFile(string filePath);
    }
}