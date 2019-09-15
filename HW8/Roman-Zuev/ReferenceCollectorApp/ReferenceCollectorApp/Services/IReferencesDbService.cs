using System.Collections.Generic;

namespace ReferenceCollectorApp.Services
{
    public interface IReferencesDbService
    {
        void WriteRefsToDb(ref Dictionary<string, int> filtered);
    }
}