using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReferenceCollectorApp.Services
{
    public interface IReferenceCollectorService
    {
        Task<string> DownloadPage(string uri, string folderPath);
        Dictionary<string, int> AddRefsToDictionary(string filePath, int iterationId/*, Dictionary<string, int> filteredUrls*/);
        void WriteDictionaryToDb(Dictionary<string, int> data);
    }
}