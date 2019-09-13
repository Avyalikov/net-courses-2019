namespace MultithreadApp.Components
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Interfaces;

    class WikiHttpProvider : IHttpProvider
    {
        public string BaseUrl { get; } = "https://en.wikipedia.org/wiki/";

        public bool IsExist(string page)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(BaseUrl + page).Result;
                return response.StatusCode == HttpStatusCode.OK;
            }
        }

        public async Task<string> GetHtmlAsync(string page)
        {
            using (var client = new HttpClient())
            {
                return  await client.GetStringAsync(BaseUrl + page);
            }
        }
    }
}