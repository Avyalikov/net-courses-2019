using System;
using System.IO;
using System.Net;

namespace ConsoleAppTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost/api/values");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                //string json = "{\"user\":\"test\"," +
                //              "\"password\":\"bla\"}";

                string json = @"{
	""PropA"": ""1234"",
	""PropB"": ""abcde"",
	""SomeSubData"": {
		""DataA"": ""A"",
		""DataB"": ""B""
    }
}";

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }
    }
}
