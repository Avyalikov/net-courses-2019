using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace stockSimulator.Client
{
    public class ClientRequests
    {
        string connectionString = "http://localhost:62094/";
        internal void ShowListOfClients()
        {
            Console.WriteLine("Select number of clients on one page: ");
            int numberOfClientsToPrint = Simutator.GetNum();
            Console.WriteLine("Select number of page to show clients: ");
            int numberOfPages = Simutator.GetNum();
            string request = connectionString + "clients?top=" + numberOfClientsToPrint + "&page=" + numberOfPages;
            Get(request);
        }

        private void Get(string url)
        {
            // Create a request for the URL.   
            WebRequest request = WebRequest.Create(url);
            WebResponse response = null;
            try
            {
                // Get the response.  
                response = request.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to get response! Error: " + ex.Message);
            }

            if (response != null)
            {
                // Display the status.  
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);

                // Get the stream containing content returned by the server. 
                // The using block ensures the stream is automatically closed. 
                using (Stream dataStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.  
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.  
                    string responseFromServer = reader.ReadToEnd();
                    // Display the content.  
                    Console.WriteLine(responseFromServer);
                }

                // Close the response.  
                response.Close();
            }

        }
    }
}
