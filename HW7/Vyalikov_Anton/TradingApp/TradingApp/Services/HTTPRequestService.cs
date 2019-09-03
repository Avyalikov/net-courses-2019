namespace TradingApp.Services
{
    using Interfaces;
    using System.Net.Http;

    class HTTPRequestService : IHTTPRequestService
    {
        private readonly HttpClient httpClient = new HttpClient();

        public HttpResponseMessage PostAsync(string url, HttpContent content)
        {
            return this.httpClient.PostAsync(url, content).Result;
        }

        public HttpResponseMessage GetAsync(string url)
        {
            return this.httpClient.GetAsync(url).Result;
        }
    }
}
