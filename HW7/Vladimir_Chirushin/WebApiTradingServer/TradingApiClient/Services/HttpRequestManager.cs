namespace TradingApiClient.Services
{
    using System.Net.Http;

    public class HttpRequestManager : IHttpRequestManager
    {
        private HttpClient httpClient = new HttpClient();

        public HttpResponseMessage PostAsync(string url, HttpContent contentString)
        {
            return this.httpClient.PostAsync(url, contentString).Result;
        }
        public HttpResponseMessage GetAsync(string url)
        {
            return this.httpClient.GetAsync(url).Result;
        }
    }
}
