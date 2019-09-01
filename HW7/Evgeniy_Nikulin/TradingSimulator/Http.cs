namespace TradingSimulator
{
    using System.IO;
    using System.Net;
    using System.Text;

    class Http
    {
        public static string Get(string url)
        {
            url = "http://" + url;

            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";

            var resp = (HttpWebResponse)req.GetResponse();
            var respStream = resp.GetResponseStream();
            var sr = new StreamReader(respStream);

            string value = sr.ReadToEnd();

            sr.Close();
            respStream.Close();
            resp.Close();

            return value;
        }

        public static string Post(string url)
        {
            url = "http://" + url;

            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = url.Length;

            byte[] data = Encoding.ASCII.GetBytes(url);

            var rs = req.GetRequestStream();
            rs.Write(data, 0, data.Length);
            rs.Close();

            var resp = (HttpWebResponse)req.GetResponse();
            var respStream = resp.GetResponseStream();
            var sr = new StreamReader(respStream);

            string value = sr.ReadToEnd();

            sr.Close();
            respStream.Close();
            resp.Close();

            return value;
        }
    }
}
