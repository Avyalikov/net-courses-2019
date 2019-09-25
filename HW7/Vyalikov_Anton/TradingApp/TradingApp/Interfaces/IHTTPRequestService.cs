namespace TradingApp.Interfaces
{
    using System.Net.Http;

    interface IHTTPRequestService
    {
        HttpResponseMessage PostAsync(string url, HttpContent content);

        HttpResponseMessage GetAsync(string url);
    }
}
