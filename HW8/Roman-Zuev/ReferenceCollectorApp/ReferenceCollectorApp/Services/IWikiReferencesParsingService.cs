namespace ReferenceCollectorApp.Services
{
    using System.Collections.Generic;
    public interface IWikiReferencesParsingService
    {
        Dictionary<string, int> ParseRefsFromFileToDictionary(string filePath, int iterationId);
    }
}