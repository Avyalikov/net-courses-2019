using System.Net.Http;
using System.Threading.Tasks;

namespace MultithreadConsoleApp.Interfaces
{
    public interface IHtmlReader
    {
        Task<string> ReadHttp(string url);
    }
}