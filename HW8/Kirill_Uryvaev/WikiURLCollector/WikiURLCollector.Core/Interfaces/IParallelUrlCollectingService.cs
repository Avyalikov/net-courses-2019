using System.Collections.Generic;
using System.Threading.Tasks;

namespace WikiURLCollector.Core.Interfaces
{
    public interface IParallelUrlCollectingService
    {
        Task<Dictionary<string, int>> GetUrls(string address, int maxIteration);
    }
}