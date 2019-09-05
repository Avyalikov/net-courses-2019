namespace Traiding.ConsoleApp.DependencyInjection
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Traiding.ConsoleApp.Dto;

    class RequestSender
    {
        private readonly HttpClient client;
        public RequestSender()
        {
            this.client = new HttpClient();
        }

        public string BaseAddress
        {
            set
            {
                this.client.BaseAddress = new Uri(value);
            }
        }

        private string Post<T>(string reqString, T content)
        {
            var jsonString = JsonConvert.SerializeObject(content);
            HttpContent contentString = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var resp = client.PostAsync(reqString, contentString).Result;
            return returnValueFromRequest(resp, out string answer);
        }

        private string returnValueFromRequest(HttpResponseMessage response, out string answer)
        {
            answer = response.ToString();
            var responseContent = response.Content.ReadAsStringAsync();
            string result = "";
            if (response.IsSuccessStatusCode)
            {
                result = responseContent.Result;
            }
            answer += responseContent.Result;
            return result;
        }

        public string AddClient(ClientInputData clientInputData)
        {
            string request = "clients/add";
            return Post(request, clientInputData);
        }
    }
}
